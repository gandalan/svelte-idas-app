// Entry for NeherApp3: the shell loads this file as a module and mounts the app inside a Shadow DOM
// in its content area. Standalone (browser, iframe) main.js is still used.
import { fluentApi } from "@gandalan/weblibs";
import { mount, unmount } from "svelte";
import App from "./App.svelte";
import { moduleName, serviceName } from "./appConfig.js";
import iconUrl from "./assets/svelte.png?url";
import cssUrl from "./index.css?url";
import { registerMissingProperties } from "./lib/shadowProperties.js";
import { idasBackend, localBackend } from "./stores";

// Absolute, because the shell loads from another origin; the Vite dev server only delivers CSS
// as a stylesheet (instead of an HMR module) with ?direct
const cssAddress = new URL(cssUrl, import.meta.url).href
    + (import.meta.env.DEV ? `${cssUrl.includes("?") ? "&" : "?"}direct` : "");

// The backend API sits next to the server this module was loaded from (wwwroot/neherapp3/ -> /api)
const apiUrl = new URL(/* @vite-ignore */ "../api", import.meta.url).href;

/** @type {import("@gandalan/weblibs").NeherApp3Module} */
export default {
    moduleName,
    useShadowDom: true,
    extraCSS: [cssAddress],
    setup: ({ neherapp3 }) => {
        neherapp3.addMenuItem({
            id: moduleName,
            text: import.meta.env.DEV ? "Svelte IDAS App (vite)" : "Svelte IDAS App",
            icon: new URL(iconUrl, import.meta.url).href,
            url: `#${moduleName}/`,
        });
    },
    mount: (node, props) => {
        // Borders/shadows inside the Shadow DOM: register Tailwind's @property rules in the shell's document if needed
        registerMissingProperties(cssAddress, `${moduleName}-properties`)
            .catch((e) => console.error("Tailwind properties could not be registered", e));

        // The token comes from the shell's auth manager
        const authManager = props.authManager ?? props.idas.authManager;
        idasBackend.set(props.idas);
        localBackend.set(fluentApi(apiUrl, authManager, serviceName));

        const app = mount(App, { target: node, props: { embedding: "neherapp3", moduleName } });
        return () => unmount(app);
    },
};
