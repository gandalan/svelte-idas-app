# Development Template for IDAS Micro Apps

Template for web applications with IDAS authentication, Svelte 5 frontend, and .NET 10 backend.

## Tech Stack

* **Frontend**: Svelte 5, Vite 8, Tailwind CSS 4, own small router (path standalone, hash in NeherApp3)
* **Backend**: .NET 10, Minimal APIs
* **Auth**: IDAS JWT authentication via @gandalan/weblibs

## Configuration

1. **Request an App Token**: You need a unique App Token from Gandalan to authenticate against IDAS. Please request one by sending an email to [dev-support@gandalan.de](mailto:dev-support@gandalan.de).
2. **Configure App Token**: Add your received app token GUID in [frontend/src/main.js](frontend/src/main.js). For local testing it can also be set as `VITE_IDAS_APP_TOKEN` in `frontend/src/.env.local` (not committed).
3. **Setup Environment Variables**:
   - Copy [backend/.env.sample](backend/.env.sample) to `backend/.env`.
   - Update the values in `backend/.env` as needed.
   - **Crucial**: Store secrets only in the `.env` file - never in `appsettings.json`.
4. **Project Naming**: Rename [backend/idas-app.csproj](backend/idas-app.csproj) and update all references if needed.

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
npm run build      # Build frontend (standalone + NeherApp3 module) and backend
npm run preview    # Run production build
```

**Note**: Scripts work cross-platform (Windows, macOS, Linux).

## Embedding in NeherApp3

Besides the standalone app (`main.js`, also usable in an iframe) the build creates a NeherApp3 module from [frontend/src/neherapp3.js](frontend/src/neherapp3.js) in `backend/wwwroot/neherapp3/svelte-idas-app.js`. The shell mounts it in a Shadow DOM, passes IDAS and the auth manager, and routes `#svelte-idas-app/...` to it. In development the shell can load `http://localhost:5000/neherapp3.js` directly (module name `svelte-idas-app-vite`).

API calls, chunks and assets are resolved relative to the module, not the host page. The backend allows cross-origin calls; restrict them via `Cors:AllowedOrigins` (see [backend/.env.sample](backend/.env.sample)).

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
