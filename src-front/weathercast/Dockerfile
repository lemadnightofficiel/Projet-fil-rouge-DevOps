FROM denoland/deno:2.4.2

ARG GIT_REVISION
ENV DENO_DEPLOYMENT_ID=${GIT_REVISION}

WORKDIR /app

COPY deno.json .
COPY deno.lock .

RUN deno install

COPY . .

RUN deno install --node-modules-dir=auto --allow-scripts=npm:rollup,npm:vite,npm:esbuild
RUN deno task build

RUN deno cache _fresh/server.js

EXPOSE 8000

CMD ["serve", "-A", "_fresh/server.js"]