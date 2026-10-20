# Development Template for IDAS Micro Apps

Template for web applications with IDAS authentication, Svelte 5 frontend and .NET 10 backend. The app runs standalone (browser, iframe) and as a module inside the NeherApp3 shell.

## Tech Stack

* **Frontend**: Svelte 5, Vite 8, Tailwind CSS 4, daisyUI 5 with the shared Gandalan theme, Lucide icons, small own router
* **Backend**: .NET 10, Minimal APIs, JWT Bearer validation of the IDAS token
* **Auth**: IDAS login via [@gandalan/weblibs](https://www.npmjs.com/package/@gandalan/weblibs)
* **Tests**: Vitest (frontend), xUnit v3 (backend)

## Project Structure

```text
backend/                     ASP.NET Core app, serves the built frontend from wwwroot
  Program.cs                 startup: options, auth, CORS, endpoints
  Data/                      API endpoints (example: CounterEndpoint)
  Infrastructure/            AppOptions (environment variables), IdasAuthentication
  .env.sample                local settings, copy to .env
frontend/
  src/main.js                standalone entry (login, theme, mount)
  src/neherapp3.js           entry as NeherApp3 module
  src/appConfig.js           service and module name
  src/routerConf.js          routes of the app
  src/lib/pages/             pages (Start page with demo content)
  src/lib/components/        reusable components
  src/lib/router.svelte.js   router (paths standalone, hash in NeherApp3)
  src/index.css              Tailwind + Gandalan theme
  vite.config.js             standalone build and dev server
  vite.neherapp3.config.js   module build
tests/IdasApp.Tests/         backend tests
```

## Configuration

1. **Request an App Token**: You need a unique App Token from Gandalan to authenticate against IDAS. Please request one by sending an email to [dev-support@gandalan.de](mailto:dev-support@gandalan.de).
2. **Configure App Token**: Add your app token GUID in [frontend/src/main.js](frontend/src/main.js). For local testing it can also be set as `VITE_IDAS_APP_TOKEN` in `frontend/src/.env.local` (not committed).
3. **Backend settings**: Copy [backend/.env.sample](backend/.env.sample) to `backend/.env` (not committed). In containers, set the same names as environment variables. Store secrets only there - never in `appsettings.json`.

   | Variable | |
   |---|---|
   | `IDAS_API_JWT_PUBLIC_KEY` | **required** - public key (RSA XML) of the IDAS environment, dev and prod differ; ask dev-support |
   | `IDAS_APP_DB_CONNECTION_STRING` | optional - database of your app; rename like `IDAS_FEEDBACK_DB_CONNECTION_STRING` in [AppOptions.cs](backend/Infrastructure/AppOptions.cs) |
   | `Cors__AllowedOrigins__0`, `__1`, ... | optional - restrict which origins may call the API (default: any) |

   The backend refuses to start if the public key is missing.
4. **Naming**: Set your names in [appConfig.js](frontend/src/appConfig.js) (service and module name), the title in [index.html](frontend/src/index.html), the menu text in [neherapp3.js](frontend/src/neherapp3.js) and rename [backend/idas-app.csproj](backend/idas-app.csproj) (plus `RootNamespace`, the solution and the Dockerfile entry point).

## Development

**Quick start:**
```bash
cd frontend
npm install
npm run dev:open
```

This starts both the .NET backend (`dotnet watch`) and the Vite dev server on port 5000.

**Available commands** (in `frontend/`):
```bash
npm run dev             # Start both servers
npm run dev:open        # Start both servers and open browser
npm run build           # Build frontend (standalone + NeherApp3 module) and backend
npm run build:frontend  # Build only the frontend into backend/wwwroot
npm run preview         # Run the production build
npm test                # Frontend tests (npm run test:watch while developing)
npm run lint            # ESLint (lint:fix to fix)
npm run svelte-check    # Type check
```

Backend tests, from the repository root:
```bash
dotnet test --solution svelte-idas-app.slnx
```

## Authentication

- The frontend signs in via the IDAS login page (weblibs `fluentIdasAuthManager`) and sends the token as `Authorization: Bearer` header. Inside NeherApp3 the shell's auth manager is used.
- The backend validates this token ([IdasAuthentication.cs](backend/Infrastructure/IdasAuthentication.cs)): signature with `IDAS_API_JWT_PUBLIC_KEY`, issuer, lifetime. **Every endpoint requires a valid token** unless marked `.AllowAnonymous()` (only `/health` and the static frontend are public).
- The claims of the token (`id`, `mandantGuid`, `rights` ...) are available in endpoints via `ClaimsPrincipal`.

## Embedding

**NeherApp3:** The build creates a module from [frontend/src/neherapp3.js](frontend/src/neherapp3.js) in `backend/wwwroot/neherapp3/svelte-idas-app.js`. The shell mounts it in a Shadow DOM, passes IDAS and the auth manager, sets the theme and routes `#<moduleName>/...` to it. In development the shell can load `http://localhost:5000/neherapp3.js` directly (module name `<serviceName>-vite`, so dev and deployment can run side by side). Inside the shell's content area, lay out with container queries (`@3xl/content:`) instead of viewport breakpoints.

**iframe / standalone:** `index.html` with `main.js`.

The NeherApp3 module is prepared but optional: the standalone app does not depend on it. Not needed? Delete `neherapp3.js` and `vite.neherapp3.config.js` and remove the second step from `build:frontend` in `package.json`.

API calls, chunks and assets are resolved relative to the module, not the host page. The backend therefore allows cross-origin calls (no cookies, so no credentials).

## Theme

[index.css](frontend/src/index.css) imports the shared theme `@gandalan/weblibs/ui/theme/neher-theme.css` (themes `neher`, `neher-dark`, `i3`, selected via `data-theme`). Use daisyUI components and theme colors (`btn-primary`, `bg-base-100`, `text-base-content` ...) instead of fixed colors, then pages follow the theme automatically. Single variables can be overridden after the import, see the comment in `index.css`.

Standalone, [theme.js](frontend/src/lib/theme.js) follows the system setting (light/dark); `?theme=i3` selects a theme for testing. The Roboto font is served locally ([fonts.css](frontend/src/fonts.css)); inside NeherApp3 the shell provides it.

## Adding Pages

1. Create a component in `frontend/src/lib/pages/`.
2. Add it to [routerConf.js](frontend/src/routerConf.js); with `title` it shows up in the standalone navigation. Placeholders like `/item/{itemGuid}` are read with `getPathParams()`.
3. Navigate with `navigate(path)` or links with `href={href(path)}` from [router.svelte.js](frontend/src/lib/router.svelte.js) - they work in both modes.
4. Call your backend via the `localBackend` store, IDAS via `idasBackend` ([stores.js](frontend/src/stores.js)).

The start page ([Start.svelte](frontend/src/lib/pages/Start.svelte)) and the component showcase are demo content - replace them with your own.

## Debugging

**Frontend:**
- Dev server: http://localhost:5000 (proxies `/api` to the backend)
- Hot reload with file polling (works in WSL)

**Backend:**
- Runs on https://localhost:7207 (HTTPS) and http://localhost:5294 (HTTP)
- Attach debugger: in VS Code "Run and Debug" > ".NET Core Attach"
- Endpoints: `/api/counter` (token required), `/health`

**Common issues:**
- Backend does not start: `IDAS_API_JWT_PUBLIC_KEY` missing in `backend/.env`
- `401` from `/api`: public key of the wrong IDAS environment (dev/prod) or expired login
- Login does not work: check the app token in `main.js` / `.env.local`
- `dotnet watch` crashes after `npm run build` while `npm run dev` is running: restart `npm run dev` (the build rewrites `wwwroot`)

## Deployment

- **Docker**: the included `Dockerfile` builds frontend and backend.

```bash
docker build -t idas-app .
docker run -d -p 8080:8080 -e IDAS_API_JWT_PUBLIC_KEY="<RSAKeyValue>...</RSAKeyValue>" idas-app
```

- **Azure App Services**: e.g. with the Azure VS Code extension; set the variables as application settings.
