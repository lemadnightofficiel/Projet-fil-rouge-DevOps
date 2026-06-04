# ─── STAGE BUILD ───────────────────────────────────────────
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src
COPY ["ProjetDevOps/ProjetDevOps.csproj", "ProjetDevOps/"]
RUN dotnet restore "ProjetDevOps/ProjetDevOps.csproj"
COPY . .
WORKDIR /src/ProjetDevOps
RUN dotnet publish "ProjetDevOps.csproj" -c Release -o /app/publish --no-restore

# ─── STAGE RUNTIME ─────────────────────────────────────────
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app

RUN useradd --no-create-home --shell /bin/false appuser

COPY --from=build /app/publish .

RUN chown -R appuser:appuser /app
USER appuser

EXPOSE 8080
ENTRYPOINT ["dotnet", "ProjetDevOps.dll"]