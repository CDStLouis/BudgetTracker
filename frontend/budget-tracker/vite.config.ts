import { defineConfig } from 'vitest/config'
import vue from '@vitejs/plugin-vue'
import tailwindcss from '@tailwindcss/vite'

export default defineConfig({
  plugins: [vue(), tailwindcss()],
  server: {
    proxy: {
      '/api': {
        target: 'http://localhost:5103',
        changeOrigin: true,
        secure: false
      }
    }
  },
  test: {
    environment: 'jsdom'
  }
})