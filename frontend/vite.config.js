import { svelte } from "@sveltejs/vite-plugin-svelte"
import { defineConfig } from "vite"

// https://vitejs.dev/config/
export default defineConfig({
    plugins: [svelte()],
    build: {
        outDir: "../../backend/wwwroot",
        emptyOutDir: true,
        target: "ESNext",
        rollupOptions: {
            output: {
                entryFileNames: "[name].js",
                assetFileNames: "[name].[ext]",
                chunkFileNames: "[name].js",
                manualChunks: undefined,
            },
        },
    },
    server: {
        port: 5000,
        proxy: {
            "/api": {
                target: "https://localhost:7207",
                secure: false,
            },
        },
    },
    root: "src",
})
