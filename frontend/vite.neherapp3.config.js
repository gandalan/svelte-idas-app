import { svelte } from "@sveltejs/vite-plugin-svelte"
import tailwindcss from "@tailwindcss/vite"
import { resolve } from "path"
import { defineConfig } from "vite"

// NeherApp3 module: src/neherapp3.js as ES library into wwwroot/neherapp3.
// Runs after the normal build (which empties wwwroot).
export default defineConfig({
    plugins: [tailwindcss(), svelte()],
    root: "src",
    // Resolve assets relative to the module (index.css sits next to svelte-idas-app.js)
    base: "./",
    // src/public belongs to the standalone build only
    publicDir: false,
    build: {
        target: "esnext",
        lib: {
            entry: resolve(import.meta.dirname, "src/neherapp3.js"),
            formats: ["es"],
            fileName: () => "svelte-idas-app.js",
        },
        outDir: "../../backend/wwwroot/neherapp3",
        emptyOutDir: true,
    },
})
