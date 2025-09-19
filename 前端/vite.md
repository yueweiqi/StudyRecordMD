## Vite,前端web构建工具
### 官网：https://cn.vite.dev/
* * * 
###### 简介
###### vite是一种新型前端构建工具，能够显著提升前端开发体验。它主要由两部分组成：
- ###### ***一个开发服务器***，它基于 原生 ES 模块 提供了 丰富的内建功能，如速度快到惊人的 模块热替换（HMR）。
- ###### 一套构建指令，它使用 Rollup 打包你的代码，并且它是预配置的，可输出用于生产环境的高度优化过的静态资源。
###### 浏览器支持
> 开发环境中：Vite 需要在支持 [原生 ES 模块动态导入](https://caniuse.com/es6-module-dynamic-import)  的浏览器中使用。
> 生产环境中：默认支持的浏览器需要支持 [通过脚本标签来引入原生 ES 模块](https://caniuse.com/es6-module) 。可以通过官方插件 [@vitejs/plugin-legacy](https://github.com/vitejs/vite/tree/main/packages/plugin-legacy) 支持旧浏览器。
###### ***Vite 需要 Node.js 版本 18+ 或 20+***
###### 开始示例
```javascript
// 构建基础vite项目
npm create vite@6.2.0 项目名称

// 构建vite模版
// npm 7+，需要添加额外的 --：
npm create vite@6.2.0 项目名称(my-vue-app) -- --template vue
npm create vite@6.2.0 项目名称(my-vue-app) -- --template vue-ts
```
###### Vite配置项
###### ***package.json***
``` javascript
"scripts": {
    "dev": "vite --config vite.config.js",
    "build": "vite build --config vite.config.js",
    "preview": "vite preview --config vite.config.js"
  },
```
###### ***vite.config.js***
```javascript
import { defineConfig } from 'vite'; 
import sirv from 'sirv';
//使用defineConfig函数可以获得类型提示
export default defineConfig({
  //项目根目录（index.html 文件所在的位置）  
  base: '/',
  //'development' 用于开发，'production' 用于构建
  mode: 'development',
  //开发或生产环境服务的公共基础路径
  root: '/',
  //静态资源服务的文件夹
  publicDir: "public",
  logLevel: 'info',
  //服务器配置
  server: {
    //启用监听热重载
    watch:true,
    ports: 3001,
     strictPort: true,//是否是严格的端口号，如果true，端口号被占用的情况下，vite会退出
    host: 'localhost',
    cors: true,//为开发服务器配置 CORS , 默认启用并允许任何源
    https: false,//是否支持http2 如果配置成true 会打开https://localhost:3001/xxx;
    //用于定义开发调试阶段生成资源的 origin
    origin: 'http://127.0.0.1:8080',
    //启用中间件
    middlewareMode: true,
    //使用sirv中间件来处理静态文件服务
    handler: sirv('public', { dev: true }),
    // 反向代理 跨域配置
    proxy: {
        '/api': {
            target: "https://xxxx.com/",
            changeOrigin: true,
            rewrite: (path) => path.replace(/^\/api/, '')
        }
    }
  },
  //构建配置
  build: {
    //默认情况下，若 outDir 在 root 目录下，则 Vite 会在构建时清空该目录。
    emptyOutDir: true,
    //指定输出路径
    outDir: "dist",
    //生成静态资源的存放路径
    assetsDir: "assets",
    //小于此阈值的导入或引用资源将内联为 base64 编码，以避免额外的 http 请求。设置为 0 可以完全禁用此项
    assetsInlineLimit: 4096,
    //启用/禁用 CSS 代码拆分
    cssCodeSplit: true,
    //构建后是否生成 source map 文件
    sourcemap: false,
    //自定义底层的 Rollup 打包配置
    rollupOptions: {
        input:{//可以配置多个，表示多入口
            index:path.resolve(__dirname,"index.html"),
            // project:resolve(__dirname,"project.html")
        },
        output:{
            // chunkFileNames:'static/js/[name]-[hash].js',
            // entryFileNames:"static/js/[name]-[hash].js",
            // assetFileNames:"static/[ext]/name-[hash].[ext]"
        }
    },
    //chunk 大小警告的限制
    chunkSizeWarningLimit: 500
  },
    //预览设置  npm run build　打包之后，会生成dist文件 然后运行npm run preview；vite会创建一个服务器来运行打包之后的文件
    preview:{
        port: 4000,//端口号
        host: 'localhost',
        open: true,//是否自动打开浏览器
    }
})
```
###### 环境变量
###### 一些内置常量
```
import.meta.env.MODE: {string} 应用运行的模式。
import.meta.env.BASE_URL: {string} 部署应用时的基本 URL。他由base 配置项决定。
import.meta.env.PROD: {boolean} 应用是否运行在生产环境（使用 NODE_ENV='production' 运行开发服务器或构建应用时使用 NODE_ENV='production' ）。
import.meta.env.DEV: {boolean} 应用是否运行在开发环境 (永远与 import.meta.env.PROD相反)。
import.meta.env.SSR: {boolean} 应用是否运行在 server 上。
```
###### Vite只有以***VITE_*** 为前缀的变量才会暴露在import.meta.env对象下
``` javascript
VITE_SOME_KEY=123
//通过import.meta.env.VITE_SOME_KEY 获取
DB_PASSWORD=foobar
```
###### .env文件
``` javascript
1 .env                # 所有情况下都会加载
2 .env.local          # 所有情况下都会加载，但会被 git 忽略
3 .env.[mode]         # 只在指定模式下加载
4 .env.[mode].local   # 只在指定模式下加载，但会被 git 忽略
优先级4>3>2>1
```
###### mode 模式
> 默认情况下，开发服务器 (dev 命令) 运行在 development (开发) 模式，而 build 命令则运行在 production (生产) 模式。
这意味着当执行 vite build 时，它会自动加载 .env.production 中可能存在的环境变量
同理可以显示指定模式和对应env文件
例如:vite build --mode staging 对应 .env.staging文件
###### NODE_ENV(process.env.NODE_ENV)指的是程序是以何种方式运行的，Mode则指的是程序运行的那种模式，两者是两种概念
|Command| NODE_ENV |Mode |
|--|--|--|
| <code>vite build<code> | <code>"production"<code>  | <code>"production"<code> |
| <code>vite build --mode development<code>| <code>"production"<code>  | <code>"development"<code> |
| <code>NODE_ENV=development vite build<code> | <code>"development"<code>  | <code>"production"<code> |
| <code>NODE_ENV=development vite build --mode development<code> | <code>"development"<code>  | <code>"development "<code> |

