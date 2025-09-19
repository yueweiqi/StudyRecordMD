import { createRouter, createWebHistory } from 'vue-router'
//引入组件
import home from './views/home/HomeIndex.vue'
import login from './views/login/LoginIndex.vue'
import CTeamInfo from "@/views/current/TeamInfo.vue"
import CurrentMatchView from '@/views/showview/CurrentMatchView.vue'
//第二部：创建路由器
const router = createRouter({
    history: createWebHistory(import.meta.env.BASE_URL),//路由器的工作模式
    routes: [
      {
        name:"Home",
        path:'/',
        component: home
      },
      {
        name:"Login",
        path:'/login',
        component: login
      },
      {
        name:"CTeamInfo",
        path:'/cTeamInfo',
        component: CTeamInfo
      },
      {
        name:"CurrentMatchView",
        path:'/CurrentMatchView',
        component: CurrentMatchView
      }
    ], // 配置路由规则的数组
  })

export default router
