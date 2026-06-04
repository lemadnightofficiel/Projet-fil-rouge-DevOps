# ─── STAGE BUILD ───────────────────────────────────────────
FROM denoland/deno:2.4.2 AS build
ARG GIT_REVISION
ENV DENO_DEPLOYMENT_ID=${GIT_REVISION}
WORKDIR /app

COPY deno.json deno.lock ./
RUN deno install \
    --node-modules-dir=auto \
    --allow-scripts=npm:rollup,npm:vite,npm:esbuild

COPY . .
RUN deno task build
RUN deno cache _fresh/server.js

# ─── STAGE RUNTIME ─────────────────────────────────────────
FROM denoland/deno:2.4.2 AS final
ARG GIT_REVISION
ENV DENO_DEPLOYMENT_ID=${GIT_REVISION}
WORKDIR /app

COPY --from=build /app/_fresh ./_fresh
COPY --from=build /app/deno.json ./
COPY --from=build /app/deno.lock ./
COPY --from=build /app/node_modules ./node_modules

USER deno
EXPOSE 8000
CMD ["deno", "serve", "-A", "_fresh/server.js"]