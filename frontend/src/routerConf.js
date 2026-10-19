import Counter from "./lib/Counter.svelte";
import IDASStatus from "./lib/IDASStatus.svelte";
import NotFound from "./lib/NotFound.svelte";

export const routes = [
    { path: "/", component: IDASStatus },
    { path: "/counter", component: Counter },
];

export const notFound = NotFound;
