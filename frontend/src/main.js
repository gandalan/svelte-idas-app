import { mount } from "svelte";
import { fetchEnvConfig, fluentApi, fluentIdasAuthManager, idasFluentApi } from "@gandalan/weblibs";
import App from "./App.svelte";
import "./index.css";

// Replace with your actual app token - assigned by Gandalan
const appToken = "<YOUR_APP_TOKEN>";
// Environment can be 'dev', 'stg', or 'prod' - determines which IDAS environment to connect to
const env = "dev";
// Service name for logging and monitoring in IDAS - should be unique to your app
const serviceName = "svelte-idas-app";

async function setupAuthAndApi() 
{
    const envConfig = await fetchEnvConfig(env);
    const authManager = await fluentIdasAuthManager(appToken, envConfig.idas).init();
    if (!authManager) {
        throw new Error("Failed to initialize auth manager");
    }

    const idas = idasFluentApi(envConfig.idas, authManager, serviceName);
    const api = fluentApi("/api", authManager, serviceName);    
    return { idas, api };
}

const { idas, api } = await setupAuthAndApi();

const app = mount(App, {
    target: document.getElementById("app"),
    props: { idas, api },
});

export default app;
