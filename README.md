# Projet Fil Rouge DevOps : Application Météo

**Équipe :** Jean-Baptiste Bodusseau, Kelyan Danis, Matthieu Caron  
**Promo :** B3 Info  
**Dépôt :** https://github.com/lemadnightofficiel/Projet-fil-rouge-DevOps.git

---

## Description du sujet

Application web permettant d'afficher la météo de la journée actuelle pour une ville donnée.  
L'utilisateur saisit une localisation et obtient les conditions météo en temps réel via une API publique (ex. OpenWeatherMap).

---

## Stack technique

| Composant | Choix | Justification |
| --------- | ----- | ------------- |
| Backend / API | .NET C# (ASP.NET Core) | Typage fort, performances, bonne intégration CI/CD |
| Frontend | Deno (Fresh ou Oak) | Runtime moderne, typage natif TypeScript |
| Base de données | MariaDB | SQL léger, compatible Docker, open source |
| Orchestration | Docker Compose → Kind (K8s) | Progression naturelle du fil rouge |

---

## Rôles dans l'équipe

| Membre | Rôle | Responsabilité principale |
| ------ | ---- | ------------------------- |
| Jean-Baptiste Bodusseau | Lead Dev | Architecture API C#, revue de code |
| Kelyan Danis | Dev | Développement frontend Deno, intégration API météo |
| Matthieu Caron | DevSecOps | Pipeline CI/CD, sécurité, manifests K8s |

---

## Objectifs du fil rouge

1. Avoir une API C# conteneurisée avec healthcheck opérationnel d'ici S3.
2. Mettre en place un pipeline CI/CD (lint, build, test, push image) d'ici S3.
3. Déployer l'ensemble sur un cluster Kind avec 2 réplicas d'ici S4.
4. Ajouter du monitoring (métriques + alertes) et produire un post-mortem d'ici S5.

---

## Jalons — état d'avancement

| Séance | Livrable | Statut |
| ------ | -------- | ------ |
| S1 | README cadrage | [X] |
| S2 | Dockerfile(s) + DB en container | [X] |
| S3 | docker-compose + CI vert | [X] |

À venir : 

Manifests K8s appliqués | ☐ |
Monitoring + post-mortem | ☐ |

---

## Architecture

```
┌─────────────┐       ┌─────────────────┐       ┌──────────────┐
│  Frontend   │─────▶│   API C# (.NET)  │─────▶│   MariaDB    │
│    Deno     │       │   /api/weather  │       │  (données)   │
│  :3000      │       │   :8080         │       │  :3306       │
└─────────────┘       └─────────────────┘       └──────────────┘
```

---

## Démarrage local

```bash
# Cloner le dépôt
git clone https://github.com/lemadnightofficiel/Projet-fil-rouge-DevOps.git
cd Projet-fil-rouge-DevOps

# Copier et remplir les variables d'environnement
cp .env.example .env
# Renseigner DB_PASSWORD, etc.

# Lancer l'ensemble
docker compose up --build -d
```

Accès :
- Frontend : http://localhost:5057
- API : http://localhost:3120
  
---

## Structure du dépôt

```
Projet-fil-rouge-DevOps/
├── src-api/          # API ASP.NET Core C#
├── src-front/        # Frontend Deno
├── bdd/              # Scripts d'initialisation MariaDB
├── .github/
│   └── workflows/    # Pipelines CI/CD
├── k8s/              # Manifests Kubernetes (à venir S4)
├── docker-compose.yml
├── .env.example
└── README.md
```

## Métriques DORA cibles

| Indicateur | Objectif |
| ---------- | -------- |
| Deployment frequency | 1× par séance minimum |
| Lead time | < 1 jour (feature → main) |
| Change failure rate | < 20% |
| MTTR | < 30 min |

