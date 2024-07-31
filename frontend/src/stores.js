import { writable } from "svelte/store";

/**
 * @type {import("svelte/store").Writable<import("@gandalan/weblibs/api/fluentApi").FluentApi>}
 */
export const idasBackend = writable();

/**
 * @type {import("svelte/store").Writable<import("@gandalan/weblibs/api/fluentApi").FluentApi>}
 * */
export const localBackend = writable();
