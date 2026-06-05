# Note d'architecture : WeatherCast

**Équipe :** Jean-Baptiste Bodusseau, Kelyan Danis, Matthieu Caron   
**Dépôt :** https://github.com/lemadnightofficiel/Projet-fil-rouge-DevOps

---

## 1. Contexte

WeatherCast est une application web permettant à un utilisateur de consulter les conditions météo en temps réel pour une ville donnée, en s'appuyant sur l'API publique OpenWeatherMap.

Le projet est développé en équipe de 3 dans un contexte pédagogique DevOps, avec comme contraintes un environnement local Docker Compose, un délai de livraison par séance, et une stack applicative volontairement légère pour mettre en valeur la chaîne Dev → Build → Ship → Run.

---

## 2. Vue des services

| Service | Rôle                   | Image / Stack     | Port |
|---------|------------------------|-------------------|------|
| `api`   | API REST météo         | .NET 10 (nightly) | 3120 |
| `front` | Interface utilisateur  | Deno / Fresh      | 5057 |
| `db`    | Stockage des données   | MariaDB           | 3306 |

---

## 3. Flux principaux

```
[Utilisateur]
      │
      ▼
[Frontend Deno :5057]
      │  réseau frontend
      ▼
[API .NET :3120]
      │  réseau backend
      ▼
[MariaDB :3306]
```

**Flux CI/CD :**

```
Push / PR sur main
      │
      ▼
GitHub Actions
  ├── Lint (dotnet format --verify-no-changes)
  ├── Tests unitaires (xUnit)
  ├── Tests d'intégration (docker compose up db + api)
  └── Build image + Push → GHCR (sur main uniquement)
```

La BDD n'est jamais exposée publiquement — elle est accessible uniquement via le réseau interne `backend`. Le frontend communique avec l'API via le réseau `frontend`, sans accès direct à la BDD.

---

## 4. Choix d'orchestration

**Outil retenu : Docker Compose**

Ce choix est justifié par :

- La simplicité de configuration pour un environnement local multi-services
- La reproductibilité en une commande : `docker compose up --build -d`
- L'adéquation avec le périmètre pédagogique du module (Docker + Compose + CI/CD)

Les réseaux sont séparés (`frontend` / `backend`) pour isoler les services et limiter la surface d'attaque. Les volumes persistent les données MariaDB entre les redémarrages.

**Stratégie de déploiement :** recréation simple des containers à chaque livraison (`docker compose up -d`). Une stratégie rolling update serait envisageable avec Docker Swarm ou Kubernetes pour éviter les coupures de service, mais dépasse le périmètre actuel du module.

---

## 5. CI/CD

**Outil :** GitHub Actions — `.github/workflows/ci.yml`

**Étapes du pipeline :**

| Job                   | Étape                                      | Condition              |
|-----------------------|--------------------------------------------|------------------------|
| `build-bdd`           | Build image MariaDB custom                 | push / PR main         |
| `lint-api`            | `dotnet format --verify-no-changes`        | push / PR main         |
| `unit-tests`          | `dotnet test` (xUnit, 3 tests)             | push / PR main         |
| `integration`         | `docker compose up db api` + curl API      | après lint + tests + bdd |
| `build-and-push-api`  | Build image Docker + push GHCR (SHA tag)   | après integration, main uniquement |

**Gestion des secrets :** tous les secrets (mot de passe BDD, connection string MariaDB) sont stockés dans GitHub Secrets et injectés comme variables d'environnement au moment du run. Aucun secret n'est présent dans le dépôt. Le fichier `.env.example` documente les variables nécessaires sans leurs valeurs.

---

## 6. Observabilité (aperçu)

L'observabilité repose actuellement sur les logs Docker natifs, accessibles via `docker compose logs [service]`, ce qui permet un diagnostic rapide en cas d'échec au démarrage ou d'erreur runtime.

Pistes envisagées :

- Exposition de métriques applicatives via un endpoint `/metrics` sur l'API .NET
- Centralisation des logs avec une stack légère (Loki + Grafana) en complément de Compose

---

## 7. Limites connues

**Limite 1 — Absence de healthchecks Compose**

`depends_on` ne garantit pas que MariaDB est prête à accepter des connexions avant que l'API démarre. En cas de démarrage lent de la BDD, l'API peut échouer au lancement.

Piste d'amélioration : ajouter un `healthcheck` sur le service `db` et `condition: service_healthy` sur `api` dans le `docker-compose.yml`.

**Limite 2 — Frontend exclu du pipeline CI/CD**

Le frontend Deno n'est pas intégré au pipeline en raison d'un fichier de configuration manquant (`config.ts`). Le build du front n'est donc pas automatisé ni validé en CI.

Piste d'amélioration : livrer le fichier manquant et ajouter un job `build-and-push-front` dans le pipeline, avec push de l'image sur GHCR.
