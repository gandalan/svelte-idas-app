// Service name for logging and monitoring in IDAS - should be unique to your app
export const serviceName = "svelte-idas-app";

// Module name in NeherApp3: also the hash prefix the shell routes to this app ("#svelte-idas-app/...").
// The Vite dev server registers under its own name so dev source and deployment can run side by side.
export const moduleName = serviceName + (import.meta.env.DEV ? "-vite" : "");
