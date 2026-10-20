import { describe, expect, it } from "vitest";
import { chooseTheme, resolveTheme, startThemeAutomation } from "./theme.js";

describe("resolveTheme", () => {
    it("follows the system setting", () => {
        expect(resolveTheme({ dark: false })).toBe("neher");
        expect(resolveTheme({ dark: true })).toBe("neher-dark");
    });

    it("lets ?theme= win for testing", () => {
        expect(resolveTheme({ search: "?theme=i3", dark: true })).toBe("i3");
    });

    it("ignores unknown themes", () => {
        expect(resolveTheme({ search: "?theme=unknown" })).toBe("neher");
    });
});

describe("chooseTheme", () => {
    it("stops following the system setting", () => {
        /** @type {(() => void) | undefined} */
        let onSystemChange;
        const darkQuery = { matches: false, addEventListener: (/** @type {string} */ _, /** @type {() => void} */ f) => (onSystemChange = f), removeEventListener: () => (onSystemChange = undefined) };
        const target = /** @type {HTMLElement} */ (/** @type {unknown} */ ({ dataset: {} }));
        const win = /** @type {Window & typeof globalThis} */ (/** @type {unknown} */ ({ location: { search: "" }, matchMedia: () => darkQuery }));

        startThemeAutomation({ target, win });
        expect(target.dataset.theme).toBe("neher");

        chooseTheme("i3", target);
        darkQuery.matches = true;
        onSystemChange?.();
        expect(target.dataset.theme).toBe("i3");
    });
});
