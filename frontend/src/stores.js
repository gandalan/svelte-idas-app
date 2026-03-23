import { writable } from "svelte/store";

/**
 * @type {import("svelte/store").Writable<import("@gandalan/weblibs").IDASFluentApi | null>}
 */
export const idasBackend = writable();

/**
 * @type {import("svelte/store").Writable<import("@gandalan/weblibs").FluentApi>}
 * */
export const localBackend = writable();
