FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build-env

RUN apt-get update \
    && apt-get install -y --no-install-recommends ca-certificates curl \
    && curl -fsSL https://deb.nodesource.com/setup_24.x | bash - \
    && apt-get install -y --no-install-recommends nodejs \
    && rm -rf /var/lib/apt/lists/* \
    && apt-get clean

WORKDIR /app

# Restore as distinct layers so they are cached as long as the dependencies do not change
COPY backend/*.csproj ./backend/
RUN cd backend && dotnet restore
COPY frontend/package.json frontend/package-lock.json ./frontend/
RUN cd frontend && npm ci

# Copy everything else, build frontend and publish a release
COPY . ./
RUN cd frontend && npm run build:frontend
RUN cd backend && dotnet publish -c Release -o out --no-restore

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final

# Install cURL for HEALTHCHECK
RUN apt-get update \
    && apt-get install -y --no-install-recommends \
        curl \
    && rm -rf /var/lib/apt/lists/* \
    && apt-get autoremove -y \
    && apt-get clean

EXPOSE 8080 8081

ENV GDL.DBConnectionString=""

VOLUME [ "/data" ]

USER app
WORKDIR /app
COPY --from=build-env /app/backend/out .

HEALTHCHECK CMD curl --fail http://localhost:8080/health || exit 1
ENTRYPOINT ["dotnet", "idas-app.dll"]
