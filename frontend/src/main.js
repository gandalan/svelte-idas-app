import { mount } from "svelte";
import { fetchEnvConfig, fluentApi, fluentIdasAuthManager, idasFluentApi } from "@gandalan/weblibs";
import App from "./App.svelte";
import { idasBackend, localBackend } from "./stores";
import "./index.css";

// Replace with your actual app token - assigned by Gandalan
const appToken = "<YOUR_APP_TOKEN>";
// Environment can be 'dev', 'stg', or 'prod' - determines which IDAS environment to connect to
const env = "dev";
// Service name for logging and monitoring in IDAS - should be unique to your app
const serviceName = "svelte-idas-app";
// The app is embedded into other applications on other hosts, so relative URLs like "/api" would hit the
// host page's server. Resolve everything against the URL this script was loaded from instead.
const appBaseUrl = new URL(".", import.meta.url);

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
