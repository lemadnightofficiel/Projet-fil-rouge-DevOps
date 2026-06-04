# ─── STAGE BUILD ───────────────────────────────────────────────
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build

WORKDIR /src

COPY ["ProjetDevOps/ProjetDevOps.csproj", "ProjetDevOps/"]

RUN dotnet restore "ProjetDevOps/ProjetDevOps.csproj"

COPY . .
WORKDIR /src/ProjetDevOps

RUN dotnet publish "ProjetDevOps.csproj" -c Release -o /app/publish

# ─── STAGE RUNTIME ─────────────────────────────────────────────
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app

EXPOSE 8080
COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "ProjetDevOps.dll"]