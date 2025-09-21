import { fileURLToPath, URL } from 'node:url'
import { resolve } from 'path';

import { defineConfig, loadEnv } from 'vite'
import vue from '@vitejs/plugin-vue'
import vueDevTools from 'vite-plugin-vue-devtools'
import AutoImport from 'unplugin-auto-import/vite'
import Components from 'unplugin-vue-components/vite'
import { ElementPlusResolver } from 'unplugin-vue-components/resolvers'

// https://vite.dev/config/
export default defineConfig(
  ({ command, mode })=>{

  console.log(command);
  console.log(mode);
  const env = loadEnv(mode, process.cwd(), '')
  return {
    //base: mode === 'production' ? `/usage/` : `/`,
    base: `/`,
    plugins: [
      vue(),
      //vueDevTools(),
      AutoImport({
        resolvers: [ElementPlusResolver()],
      }),
      Components({
        resolvers: [ElementPlusResolver()],
      }),
      //basicSsl(),
    ],
    resolve: {
      alias: {
        '@2': fileURLToPath(new URL('./src', import.meta.url)),
        '@': resolve(__dirname, 'src')
      },
    },
    // https://blog.csdn.net/weixin_43422861/article/details/140296257 启用https服务
    https:true,
    server: {
      port:5176,
      proxy: {
          // 匹配请求的一整个路径的前面是否有符合的，和调用String.startsWith方法一样
          // 比如上面请求 `'/api/admin/logout'.startsWith('/api')`,所以匹配成功
          '/api': {
              target: env.VITE_API_BASE_URL, // 代理的后端给的目标地址
              // 开发模式，默认的127.0.0.1,开启后代理服务会把origin修改为目标地址
              changeOrigin: true,
              secure: true, // 是否https接口,这里不清楚,一般设置成false,就可以进行访问
              ws: true, // 是否代理websockets
              // 路径重写 ****
              // 例子：
              // 没有写 路径重写之前 的请求: http://localhost:8000/api/admin/logout
              //  写了  路径重写之后 的请求：你写的target + admin/logout
              rewrite: (path) => path.replace('/api', '')
          }
      }
    }
  }
})
