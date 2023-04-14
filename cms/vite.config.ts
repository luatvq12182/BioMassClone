import { defineConfig } from "vite";
import react from "@vitejs/plugin-react";
import path from "path";

// https://vitejs.dev/config/
export default defineConfig({
    plugins: [react()],
    resolve: {
        alias: {
            "@": path.resolve(__dirname, "./src"),
        },
    },
    server: {
        watch: {
            usePolling: true,
        },
        host: "0.0.0.0", // needed for the Docker Container port mapping to work
        strictPort: true,
        port: 5173, // you can replace this port with any port
    },
    build: {
        chunkSizeWarningLimit: 5000000,
    },
});
