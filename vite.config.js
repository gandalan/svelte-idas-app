import { defineConfig } from 'vite'
import { svelte } from '@sveltejs/vite-plugin-svelte'

// https://vitejs.dev/config/
export default defineConfig({
    plugins: [svelte()],
    build: {
        outDir: "wwwroot",
        emptyOutDir: true,
        rollupOptions: {
            output: {
                assetFileNames: "assets/[name].[ext]",
                chunkFileNames: "assets/[name].[ext]",
                entryFileNames: "assets/[name].js",
            },
        },
        write: true
    },
    server: {
        port: 5000,
        proxy: {
          '/api': {
                target : 'https://localhost:7207', 
                secure: false
            }
        }
    },
    publicDir: "wwwroot"
})
