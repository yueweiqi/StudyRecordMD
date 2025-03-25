import { dirname, resolve } from 'node:path'
import { fileURLToPath } from 'node:url'
import { defineConfig } from 'vite'
const __dirname = dirname(fileURLToPath(import.meta.url))
export default defineConfig({
    server: {
        port: 12734,
        open: true,
    },
    build:{
       rollupOptions:{
         input:{
            main:resolve(__dirname,"index.html")
         }
       },
       watch: {
          // 热重载 // https://rollupjs.org/configuration-options/#watch
        },        
    },
    preview:{
      rollupOptions: {
        input:{//可以配置多个，表示多入口
            main:"index.html",
            // project:resolve(__dirname,"project.html")
        },
        output:{
            // chunkFileNames:'static/js/[name]-[hash].js',
            // entryFileNames:"static/js/[name]-[hash].js",
            // assetFileNames:"static/[ext]/name-[hash].[ext]"
        }
    },
    }
})