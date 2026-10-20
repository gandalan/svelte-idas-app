import Counter from "./lib/Counter.svelte";
import IDASStatus from "./lib/IDASStatus.svelte";
import NotFound from "./lib/NotFound.svelte";
import Start from "./lib/pages/Start.svelte";

// "title" is shown in the navigation; routes without title (e.g. with placeholders) are not listed there
export const routes = [
    { path: "/", component: Start, title: "Start" },
    { path: "/mandant", component: IDASStatus, title: "Mandant" },
    { path: "/counter", component: Counter, title: "Counter" },
];

export const notFound = NotFound;
