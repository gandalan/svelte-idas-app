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
})
