import { createRouter, createWebHistory } from 'vue-router'
//引入组件
import home from './views/home/HomeIndex.vue'
import login from './views/login/LoginIndex.vue'
import CTeamInfo from "@/views/current/TeamInfo.vue"
import CurrentMatchView from '@/views/showview/CurrentMatchView.vue'
import CurrentVideoView from '@/views/showview/CurrentVideoView.vue'
import CurrentMatchPlayerCommentView from './views/showview/CurrentMatchPlayerCommentView.vue'
//第二部：创建路由器
const router = createRouter({
    history: createWebHistory(),//路由器的工作模式
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
      },
      {
        name:"CurrentVideoView",
        path:'/CurrentVideoView',
        component: CurrentVideoView
      },
      {
        name:"CurrentMatchPlayerCommentView",
        path:'/CurrentMatchPlayerCommentView',
        component: CurrentMatchPlayerCommentView
      }
    ], // 配置路由规则的数组
  })

export default router
