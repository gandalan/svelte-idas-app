FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build-env

RUN apt-get update \
    && apt-get install -y libpng-dev libjpeg-dev curl libxi6 build-essential libgl1-mesa-glx \
    && curl -sL https://deb.nodesource.com/setup_lts.x | bash - \
    && apt-get install -y nodejs

WORKDIR /App
# Copy everything
COPY . ./
# Restore as distinct layers
RUN dotnet restore
RUN npm install && npm run build
# Build and publish a release
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /App
COPY --from=build-env /App/out .
EXPOSE 80
VOLUME [ "/data" ]
ENV GDL.DBConnectionString=""
ENTRYPOINT ["dotnet", "idas-app.dll"]
