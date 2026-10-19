import { mount } from "svelte";
import { fetchEnvConfig, fluentApi, fluentIdasAuthManager, idasFluentApi } from "@gandalan/weblibs";
import App from "./App.svelte";
import { serviceName } from "./appConfig.js";
import { idasBackend, localBackend } from "./stores";
import "./index.css";

// Standalone entry (browser, iframe). Inside the NeherApp3 shell neherapp3.js is loaded instead.

// Replace with your actual app token - assigned by Gandalan
// (for local testing it can also be set as VITE_IDAS_APP_TOKEN in src/.env.local, which is not committed)
const appToken = import.meta.env.VITE_IDAS_APP_TOKEN || "<YOUR_APP_TOKEN>";
// Environment can be 'dev', 'stg', or 'prod' - determines which IDAS environment to connect to
const env = "dev";
// The app is embedded into other applications on other hosts, so relative URLs like "/api" would hit the
// host page's server. Resolve everything against the URL this script was loaded from instead.
const appBaseUrl = new URL(/* @vite-ignore */ ".", import.meta.url);

async function setupAuthAndApi() {
    const envConfig = await fetchEnvConfig(env);
    const authManager = await fluentIdasAuthManager(appToken, envConfig.idas).init();
    if (!authManager) {
        throw new Error("Failed to initialize auth manager");
    }

    const idas = idasFluentApi(envConfig.idas, authManager, serviceName);
    const api = fluentApi(new URL("api", appBaseUrl).href, authManager, serviceName);
    return { idas, api };
}

const { idas, api } = await setupAuthAndApi();

// Set before mounting so all components can use the backends during their initialization
idasBackend.set(idas);
localBackend.set(api);

const target = document.getElementById("app");
if (!target) {
    throw new Error("Mount target #app not found");
}

const app = mount(App, { target });

export default app;
