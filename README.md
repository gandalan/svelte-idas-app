# Development Template for IDAS Micro Apps

Template for web applications with IDAS authentication, Svelte 5 frontend, and .NET 10 backend.

## Tech Stack

* **Frontend**: Svelte 5, Vite 7, Tailwind CSS, svelte-mini-router
* **Backend**: .NET 10, Minimal APIs
* **Auth**: IDAS JWT authentication via @gandalan/weblibs

## Configuration

1. **Request an App Token**: You need a unique App Token from Gandalan to authenticate against IDAS. Please request one by sending an email to [dev-support@gandalan.de](mailto:dev-support@gandalan.de).
2. **Configure App Token**: Add your received app token GUID in [frontend/src/main.js](frontend/src/main.js#L7).
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
