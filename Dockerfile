FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env

RUN apt-get update \
    && apt-get install -y libpng-dev libjpeg-dev curl libxi6 build-essential libgl1-mesa-glx \
    && curl -sL https://deb.nodesource.com/setup_lts.x | bash - \
    && apt-get install -y nodejs

WORKDIR /app
# Copy everything
COPY . ./
# Restore as distinct layers
RUN cd backend && dotnet restore
RUN cd frontend && npm install && npm run build
# Build and publish a release
RUN cd backend && dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 as final

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

HEALTHCHECK CMD curl --fail http://localhost:8080/health
ENTRYPOINT ["dotnet", "idas-app.dll"]
