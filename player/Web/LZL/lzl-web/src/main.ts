import './assets/main.css'

import { createApp } from 'vue'
import App from './App.vue'
//pinia状态
import { createPinia } from 'pinia'
import { createPersistedState } from 'pinia-plugin-persistedstate';
//引入bootstrap
import 'bootstrap/dist/css/bootstrap.min.css'
import router from './router'
import elementPlus from 'element-plus'
import 'element-plus/dist/index.css'

const app=createApp(App);

app.use(router);
app.use(elementPlus);

// 使用 Pinia
const pinia = createPinia()
pinia.use(createPersistedState())
app.use(pinia)

app.mount('#app')

const arr1:{name: string, age:number} ={
  name:"",age:1
};
console.log(arr1);
