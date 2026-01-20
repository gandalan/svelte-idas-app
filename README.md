# Development Template for IDAS Micro Apps

Template for web applications with IDAS authentication, Svelte 5 frontend, and .NET 10 backend.

## Tech Stack

* **Frontend**: Svelte 5, Vite 7, Tailwind CSS, svelte-mini-router
* **Backend**: .NET 10, Minimal APIs
* **Auth**: IDAS JWT authentication via @gandalan/weblibs

## Configuration

1. Add your app token GUID in `frontend/src/main.js` (line 7)
2. Store secrets in `.env` file (sample provided) - never in `appsettings.json`
3. Rename `idas-app.csproj` and update all references if needed

## Development

**Quick start:**
```bash
cd frontend
npm install
npm run dev:open
```

This starts both the .NET backend (`dotnet watch`) and Vite dev server concurrently on port 5000.

**Available commands:**
```bash
npm run dev        # Start both servers
npm run dev:open   # Start both servers and open browser
npm run build      # Build frontend and backend
npm run preview    # Run production build
```

**Note**: Scripts work cross-platform (Windows, macOS, Linux).

## Debugging

**Frontend (Svelte):**
- Dev server: http://localhost:5000 (proxies `/api` to backend)
- Browser DevTools: Press F12, check Console and Network tabs
- Hot reload: Enabled with file polling (works in WSL)

**Backend (.NET):**
- Runs on https://localhost:7207 (HTTPS) and http://localhost:5294 (HTTP)
- Attach debugger: In VS Code, use "Run and Debug" > ".NET Core Attach"
- Watch mode: `dotnet watch` auto-rebuilds on file changes
- API endpoints: `/api/counter`, `/health`

**Common issues:**
- Hot reload not working: Polling enabled in `vite.config.js` for WSL compatibility
- Auth errors: Check app token GUID in `main.js`
- CORS errors: Backend proxy configured in `vite.config.js`

## Deployment

- **Azure App Services**: Use Azure VS Code extension
- **Docker**: Use included `Dockerfile` (builds Node.js frontend and .NET backend)

```bash
docker build -t idas-app .
docker run -d -p 8080:8080 idas-app
```
