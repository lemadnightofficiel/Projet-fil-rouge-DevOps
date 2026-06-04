FROM denoland/deno:latest

# Argument de version (optionnel, pour suivre la version de l'app)
ARG GIT_REVISION
ENV DENO_DEPLOYMENT_ID=${GIT_REVISION}

# Répertoire de travail dans le conteneur
WORKDIR /app

# Copier les fichiers de dépendances en premier (pour optimiser le cache Docker)
COPY deno.json .
COPY deno.lock . 2>/dev/null || true

# Installer les dépendances
RUN deno install

# Copier le reste du code source
COPY . .

# Build de l'application Fresh ( generation du dossier _fresh )
RUN deno task build

# Cache du serveur pré-buildé
RUN deno cache _fresh/server.js

# Exposer le port utilisé par Fresh (par défaut 8000)
EXPOSE 8000

# Commande de démarrage
CMD ["serve", "-A", "_fresh/server.js"]