# Development Template for IDAS micro apps

**Goal**: this template enables developers to quickly set up a web application with the following
features included:

* Authentication against the IDAS backend (providing a JWT token)
* Sample Svelte frontend code
* App-local .NET 8 backend

## Configuration

* Take care to change all occurrences of "idas-app" in your files when you rename idas-app.csproj (you should!)
* appsettings.json should NOT contain secrets. Store secrets (i.e. connection strings) in a `.env` file (sample provided)

## Deployment

The app can be deployed to Azure App Services as it is (use the Azure VSCode extension!), or dockerized using the included sample Dockerfile.
