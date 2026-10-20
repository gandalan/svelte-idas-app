// Standalone the app uses real paths; embedded in NeherApp3 the URL belongs to the shell, which routes
// "#<moduleName>/..." to the module - so there the route lives in the hash behind the module prefix.

/** @typedef {{ path: string, component: any }} Route */
/** @typedef {{ mode: "path" | "hash", prefix?: string }} RouterMode */
/** @typedef {Record<string, string>} Query */

export const router = $state(/** @type {{ path: string, search: string, params: Record<string, string>, component: any }} */ ({
    path: "/",
    search: "",
    params: {},
    component: undefined,
}));

/** @type {{ mode: "path" | "hash", prefix: string, routes: Route[], notFound: any }} */
let settings = { mode: "path", prefix: "", routes: [], notFound: undefined };

/**
 * @param {RouterMode} mode
 * @param {{ pathname?: string, search?: string, hash?: string }} location
 */
export function readLocation({ mode, prefix = "" }, location) {
    if (mode !== "hash") {
        return { path: location.pathname || "/", search: location.search ?? "" };
    }
    const hash = (location.hash ?? "").replace(/^#/, "");
    // "#my-app" counts like "#my-app/"
    const name = prefix.replace(/\/$/, "");
    const rest = hash.slice(name.length);
    // belongs to another module of the shell (also "#my-app-other/..."): not ours
    if (!hash.startsWith(name) || (name && rest && !/^[/?]/.test(rest))) {
        return null;
    }
    const [path, query] = rest.split("?");
    return { path: `/${path.replace(/^\/+/, "")}`, search: query ? `?${query}` : "" };
}

/**
 * @param {RouterMode} mode
 * @param {string} path
 * @param {Query} [query]
 */
export function buildAddress({ mode, prefix = "" }, path, query) {
    // only builds a string, nothing reactive
    // eslint-disable-next-line svelte/prefer-svelte-reactivity
    const qs = query && Object.keys(query).length ? `?${new URLSearchParams(query)}` : "";
    return mode === "hash" ? `#${prefix}${path.replace(/^\/+/, "")}${qs}` : path + qs;
}

/**
 * Route paths may contain placeholders like "/item/{itemGuid}"
 * @param {Route[]} routes
 * @param {string} path
 */
export function findRoute(routes, path) {
    const parts = path.split("/").filter(Boolean);
    for (const route of routes) {
        const pattern = route.path.split("/").filter(Boolean);
        if (pattern.length !== parts.length) {
            continue;
        }
        /** @type {Record<string, string>} */
        const params = {};
        const matches = pattern.every((p, i) => {
            if (p.startsWith("{") && p.endsWith("}")) {
                // broken encoding leads to "not found" instead of a white page
                try {
                    params[p.slice(1, -1)] = decodeURIComponent(parts[i]);
                } catch {
                    return false;
                }
                return true;
            }
            return p === parts[i];
        });
        if (matches) {
            return { route, params };
        }
    }
    return { route: undefined, params: {} };
}

// On start without an own hash show the start page; afterwards ignore hash changes of other modules
function update(start = false) {
    const location = readLocation(settings, window.location) ?? (start ? { path: "/", search: "" } : null);
    if (!location) {
        return;
    }
    const { route, params } = findRoute(settings.routes, location.path);
    router.path = location.path;
    router.search = location.search;
    router.params = params;
    router.component = route?.component ?? settings.notFound;
}

/**
 * Starts the router and returns a function that stops it again.
 * @param {{ mode?: "path" | "hash", prefix?: string, routes: Route[], notFound?: any }} options
 */
export function startRouter({ mode = "path", prefix = "", routes, notFound }) {
    settings = { mode, prefix, routes, notFound };
    update(true);
    const event = mode === "hash" ? "hashchange" : "popstate";
    const onChange = () => update();
    window.addEventListener(event, onChange);
    return () => window.removeEventListener(event, onChange);
}

/**
 * @param {string} path
 * @param {Query} [query]
 */
export function navigate(path, query) {
    const target = buildAddress(settings, path, query);
    if (settings.mode === "hash") {
        window.location.hash = target;
    } else {
        history.pushState(null, "", target);
        update();
    }
}

/**
 * onclick for links to own pages (<a href={href(...)} onclick={followLink}>): in path mode navigates
 * without a full page reload; the hash mode is handled by the browser
 * @param {MouseEvent & { currentTarget: HTMLAnchorElement }} event
 */
export function followLink(event) {
    if (settings.mode !== "path" || event.defaultPrevented || event.button !== 0
        || event.ctrlKey || event.metaKey || event.shiftKey || event.altKey) {
        return;
    }
    // only read once, nothing reactive
    // eslint-disable-next-line svelte/prefer-svelte-reactivity
    const url = new URL(event.currentTarget.href);
    if (url.origin !== window.location.origin) {
        return;
    }
    event.preventDefault();
    history.pushState(null, "", url.pathname + url.search);
    update();
}

/**
 * For href attributes: in hash mode "/" must point to the module's start page, not the shell's root
 * @param {string} path
 * @param {Query} [query]
 */
export function href(path, query) {
    return buildAddress(settings, path, query);
}

export function getPathParams() {
    return router.params;
}
