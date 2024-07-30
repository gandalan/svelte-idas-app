# Development Template for IDAS micro apps

**Goal**: this template enables developers to quickly set up a web application with the following
features included:

* Authentication against the IDAS backend (providing a JWT token)
* Sample Svelte frontend code
* App-local .NET 8 backend

## Configuration

* Take care to change all occurrences of "idas-app" in your files when you rename idas-app.csproj (you should!)
* appsettings.json should NOT contain secrets. Store secrets (i.e. connection strings) in a `.env` file (sample provided)
* Add your app token in `frontend/src/main.js`

## Deployment

The app can be deployed to Azure App Services as it is (use the Azure VSCode extension!), or dockerized using the included sample Dockerfile.

## Quickstart
1. Add app token to frontend/src/main.js
1. `cd frontend && npm install && npm run dev:open`
