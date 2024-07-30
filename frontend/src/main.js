import { authBuilder, fetchEnv, idasApi } from "@gandalan/weblibs";
import { localApi } from "@gandalan/weblibs/api/fluentApi";
import App from "./App.svelte";
import "./index.css";

const appToken = "<INSERT YOUR APP TOKEN HERE>";
const env = "dev";

// needed for post authentication redirect
const urlParams = new URLSearchParams(location.search);
if (urlParams.has("t")) {
    localStorage.setItem("idas-refresh-token", urlParams.get("t") || "");
    urlParams.delete("t");
    location.search = urlParams.toString(); // this will cause a page reload!
}

const refreshToken = localStorage.getItem("idas-refresh-token");
const envConfig = await fetchEnv(env);

await authBuilder()
    .useAppToken(appToken)
    .useRefreshToken(refreshToken)
    .useBaseUrl(envConfig.idas)
    .init();

const idas = idasApi(appToken)
    .useEnvironment(envConfig);

const local = localApi();

const app = new App({
    target: document.getElementById("app"),
    props: { idas, local },
});

export default app;
