import { svelte } from "@sveltejs/vite-plugin-svelte"
import tailwindcss from "@tailwindcss/vite"
import { defineConfig } from "vite"

// https://vite.dev/config/
export default defineConfig({
    plugins: [tailwindcss(), svelte()],
    build: {
        outDir: "../../backend/wwwroot",
        emptyOutDir: true,
        target: "esnext",
        rolldownOptions: {
            output: {
                entryFileNames: "[name].js",
                assetFileNames: "[name].[ext]",
                chunkFileNames: "[name].js",
            },
        },
    },
    server: {
        port: 5000,
        watch: {
            usePolling: true,
        },
        proxy: {
            "/api": {
                target: "https://localhost:7207",
                secure: false,
            },
        },
    },
    root: "src",
    experimental: {
        // Load chunks and assets relative to the module itself, not the host page, so the app also works
        // when embedded into another application. index.html keeps absolute URLs for SPA deep links.
        renderBuiltUrl: (filename, { hostType }) => hostType === "html" ? undefined : { relative: true },
    },
})
