export const routerConf = {
    routes: [
        {
            path: "/",
            render: () => import("./lib/IDASStatus.svelte")
        },
        {
            path: "/counter",
            render: () => import("./lib/Counter.svelte")
        }
    ]
}
