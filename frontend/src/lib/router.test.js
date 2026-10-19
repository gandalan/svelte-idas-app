import { describe, expect, it } from "vitest";
import { buildAddress, findRoute, readLocation } from "./router.svelte.js";

const routes = [
    { path: "/", component: "Start" },
    { path: "/counter", component: "Counter" },
    { path: "/item/{itemGuid}", component: "Item" },
];

describe("findRoute", () => {
    it("finds fixed routes and reads placeholders", () => {
        expect(findRoute(routes, "/").route?.component).toBe("Start");
        expect(findRoute(routes, "/counter").params).toEqual({});
        expect(findRoute(routes, "/item/f%201")).toMatchObject({ route: { component: "Item" }, params: { itemGuid: "f 1" } });
    });

    it("returns no route for unknown paths", () => {
        expect(findRoute(routes, "/does/not/exist").route).toBeUndefined();
    });

    it("treats broken encoding as not found instead of throwing", () => {
        expect(findRoute(routes, "/item/%E0%A4").route).toBeUndefined();
    });
});

describe("readLocation", () => {
    it("standalone takes path and query from the address", () => {
        expect(readLocation({ mode: "path" }, { pathname: "/item/f1", search: "?t=abc" })).toEqual({ path: "/item/f1", search: "?t=abc" });
    });

    it("in NeherApp3 takes the part of the hash behind the module name", () => {
        const mode = /** @type {const} */ ({ mode: "hash", prefix: "my-app/" });
        expect(readLocation(mode, { hash: "#my-app/item/f1" })).toEqual({ path: "/item/f1", search: "" });
        expect(readLocation(mode, { hash: "#my-app/counter?x=1" })).toEqual({ path: "/counter", search: "?x=1" });
        expect(readLocation(mode, { hash: "#my-app/" })).toEqual({ path: "/", search: "" });
    });

    it("ignores hashes of other modules", () => {
        expect(readLocation({ mode: "hash", prefix: "my-app/" }, { hash: "#other-module/" })).toBeNull();
    });
});

describe("buildAddress", () => {
    it("builds paths standalone and hashes in NeherApp3", () => {
        expect(buildAddress({ mode: "path" }, "/counter", { x: "1" })).toBe("/counter?x=1");
        expect(buildAddress({ mode: "hash", prefix: "my-app/" }, "/counter")).toBe("#my-app/counter");
        expect(buildAddress({ mode: "hash", prefix: "my-app/" }, "/")).toBe("#my-app/");
    });
});
