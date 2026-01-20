import { mount } from "svelte";
import { authBuilder, fetchEnv, idasApi } from "@gandalan/weblibs";
import { localApi } from "@gandalan/weblibs/api/fluentApi";
import App from "./App.svelte";
import "./index.css";

const appToken = "<YOUR_APP_TOKEN>";
const env = "dev";

// needed for post authentication redirect
const urlParams = new URLSearchParams(location.search);
if (urlParams.has("t")) {
    localStorage.setItem("idas-refresh-token", urlParams.get("t") || "");
    urlParams.delete("t");
    location.search = urlParams.toString(); // this will cause a page reload!
}

const refreshToken = localStorage.getItem("idas-refresh-token");

let idas = null;
let envConfig = null;

// Only initialize IDAS if a valid app token is provided
if (appToken !== "<INSERT YOUR APP TOKEN HERE>") {
    try {
        envConfig = await fetchEnv(env);

        const auth = authBuilder();
        if (auth) {
            await auth
                .useAppToken(appToken)
                .useRefreshToken(refreshToken)
                .useBaseUrl(envConfig.idas)
                .init();
        }

        idas = idasApi(appToken)
            .useEnvironment(envConfig);
    } catch (error) {
        console.error("Failed to initialize IDAS authentication:", error);
    }
}

const local = localApi();

const app = mount(App, {
    target: document.getElementById("app"),
    props: { idas, local },
});

export default app;
