export const THEMES = ["neher", "neher-dark", "i3"];

/**
 * ?theme=... wins (for testing), otherwise light/dark following the system setting like in NeherApp3
 * @param {{ search?: string, dark?: boolean }} [options]
 */
export function resolveTheme({ search = "", dark = false } = {}) {
    const fromAddress = new URLSearchParams(search).get("theme");
    if (fromAddress && THEMES.includes(fromAddress)) {
        return fromAddress;
    }
    return dark ? "neher-dark" : "neher";
}

let stopAutomation = () => {};

/**
 * Standalone only (inside NeherApp3 the shell sets data-theme): sets data-theme on <html> and follows
 * changes of the system setting. ?theme= is only read on start, navigating drops the query.
 * Returns a cleanup function.
 */
export function startThemeAutomation({ target = document.documentElement, win = window } = {}) {
    const search = win.location.search;
    const darkQuery = win.matchMedia("(prefers-color-scheme: dark)");
    const apply = () => {
        target.dataset.theme = resolveTheme({ search, dark: darkQuery.matches });
    };
    apply();
    darkQuery.addEventListener("change", apply);
    stopAutomation = () => darkQuery.removeEventListener("change", apply);
    return stopAutomation;
}

/**
 * Theme chosen by hand: wins over the system setting from now on
 * @param {string} theme
 * @param {HTMLElement} [target]
 */
export function chooseTheme(theme, target = document.documentElement) {
    stopAutomation();
    target.dataset.theme = theme;
}
