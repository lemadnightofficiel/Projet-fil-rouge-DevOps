# Fresh project

Your new Fresh project is ready to go. You can follow the Fresh "Getting
Started" guide here: https://fresh.deno.dev/docs/getting-started

### Usage

Make sure to install Deno:
https://docs.deno.com/runtime/getting_started/installation

Make shure you have create the config.ts in the same folder as main.ts,
inside set the apiulr config with your api url.
```
export const config = {
    apiulr: "https://your_weather_api",
} satisfies Record<string, unknown>;
```

Then start the project in development mode:

```
deno task dev
```

To deploy the projet:
You need to have docket instaled.
```
//build docker image
docker build --build-arg GIT_REVISION=$(git rev-parse HEAD) -t my-fresh-app .

//launch contener
docker run -t -i -p 80:8000 my-fresh-app
```

This will watch the project directory and restart as necessary.
