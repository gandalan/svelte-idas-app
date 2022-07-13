#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

ENV GDL.DBConnectionString=""

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
RUN apt update \
    && curl -fsSL https://deb.nodesource.com/setup_lts.x | bash \
    && apt -y install nodejs
WORKDIR /src
COPY ["IDASAuthManager.csproj", ""]
RUN dotnet restore "./IDASAuthManager.csproj"
COPY . .
WORKDIR "/src/."
RUN npm install && npm run build

FROM build AS publish
RUN dotnet publish "IDASAuthManager.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "idas-app.dll"]

