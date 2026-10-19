import { describe, expect, it } from "vitest";
import { resolveTheme } from "./theme.js";

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
