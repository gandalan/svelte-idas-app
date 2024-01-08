import { initIDAS } from "@gandalan/weblibs";
import App from "./App.svelte";
import "./index.css";

let app;

initIDAS(<INSERT YOUR AUTH TOKEN HERE>).then(settings =>
{
    app = new App({
        target: document.getElementById("app"),
        props: { settings },
    });
})

export default app
