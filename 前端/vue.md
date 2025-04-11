## Vue
### 官网：https://cn.vuejs.org/
#### vue核心内容分为三个模块
#### 1.声明式渲染，通过模版语法可以声明式地描述最终输出的 HTML 和 JavaScript 状态之间的关系。
#### 2.响应式变量，基于mvvm模式实现，通过操作声明的响应式变量（视图模型viewmode）实现js变量（数据模型model）和html标签（视图View）的双向数据绑定，以及事件驱动和命令绑定
> MVVM模式三个优势
> 解耦:view和model可以相互独立开发，降低耦合度
> 可测性:可针对于viewmode进行测试，不用太考虑其他层的实现
> 可维护性:模型和视图相互独立，后续的开发或者迭代可以只修改对应的业务代码，相对隔离
#### 3.单文件组件，


``` javascript
//安装路由组件
npm view vue-router versions
npm install vue-router@4.5.0
```
在src文件夹下新建router文件夹，并在router里新建index.ts的文件。
index.ts文件
``` javascript
// 第一步：引入createRouter
import { createRouter, createWebHistory } from 'vue-router'
//引入组件
import home from '../views/login/LoginIndex.vue'

 
//第二部：创建路由器
const router = createRouter({
    history: createWebHistory(),//路由器的工作模式
    routes: [
      {
        name:"Login",
        path:'/Login',
        component: home
      }
    ], // 配置路由规则的数组
  })
  
export default router
```
修改main.ts
``` javascript
//引入路由器
import router from './router/index.ts'

const app=createApp(App);
app.use(router);
app.mount('#app')
```

修改tsconfig.app.json
``` javascript
{
  "extends": "@vue/tsconfig/tsconfig.dom.json",
  "compilerOptions": {
    "tsBuildInfoFile": "./node_modules/.tmp/tsconfig.app.tsbuildinfo",

    /* Linting */
    "strict": true,
    "noUnusedLocals": true,
    "noUnusedParameters": true,
    "noFallthroughCasesInSwitch": true,
    "noUncheckedSideEffectImports": true,
    "moduleResolution": "node"  //要加这个选项
  },
  "include": ["src/**/*.ts", "src/**/*.tsx", "src/**/*.vue"],  
}
```
``` javascript
//安装bootstrap框架
npm view bootstrap versions
npm install bootstrap@5.2.0 
//安装element-plus
npm install element-plus@2.9.0
```
main.ts
``` javascript
import { createApp } from 'vue'
import './style.css'
import App from './App.vue'
//引入路由器
import router from './router/index.ts'
//引入bootstrap
import 'bootstrap/dist/css/bootstrap.min.css'
import 'bootstrap/dist/js/bootstrap.min.js'
//引入element-plus
import ElementPlus from 'element-plus'
import 'element-plus/dist/index.css'

const app=createApp(App);
//路由
app.use(router);
//element-plus
app.use(ElementPlus);
app.mount('#app')


```