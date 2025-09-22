<template>
  <el-row class="h-100">
    <el-col :span="4" style="border-right: 1px solid #9e9e9e52;">
      <h4 class="mb-2 py-2 ps-2">LZL</h4>
      <el-menu
        default-active="1"
        class="el-menu-vertical-demo"
        @open="handleOpen"
        @close="handleClose"
        style="border-right: 0px;"
      >
      <el-sub-menu index="1">
        <template #title>
            <el-icon><icon-menu /></el-icon>
            <span>基础管理</span>
          </template>
        <el-menu-item index="1-1" @click="menuItemClick(PlayerView)">
          <el-icon><icon-menu /></el-icon>
          <span>选手管理</span>
        </el-menu-item>
        <el-menu-item index="1-2" @click="menuItemClick(TeamView)">
          <el-icon><icon-menu /></el-icon>
          <span>队伍管理</span>
        </el-menu-item>
        <el-menu-item index="1-3" @click="menuItemClick(LegendView)">
          <el-icon><icon-menu /></el-icon>
          <span>英雄管理</span>
        </el-menu-item>
      </el-sub-menu>
      <el-sub-menu index="2" >
        <template #title>
            <el-icon><icon-menu /></el-icon>
            <span>比赛管理</span>
          </template>
        <el-menu-item index="2-1" @click="menuItemClick(CurrentMatchView)">
          <el-icon><icon-menu /></el-icon>
          <span>比赛管理</span>
        </el-menu-item>
        <el-menu-item index="2-2" @click="menuItemClick(MatchPlayerCommentView)">
          <el-icon><icon-menu /></el-icon>
          <span>选手评论</span>
        </el-menu-item>
        <el-menu-item index="2-3" @click="menuItemClick(VideoView)">
          <el-icon><icon-menu /></el-icon>
          <span>视频管理</span>
        </el-menu-item>
      </el-sub-menu>

        <el-sub-menu index="3">
          <template #title>
            <el-icon><icon-menu /></el-icon>
            <span>直播展示</span>
          </template>
          <el-menu-item-group title="直播展示">
            <el-menu-item index="3-1" @click="menuItemOutClick('/CurrentMatchView')">
              <el-icon><icon-menu /></el-icon>
              <span>选手信息展示</span>
            </el-menu-item>
            <el-menu-item index="3-2" @click="menuItemOutClick('/CurrentMatchPlayerCommentView')">
              <el-icon><icon-menu /></el-icon>
              <span>选手评论展示</span>
            </el-menu-item>
            <el-menu-item index="3-2" @click="menuItemOutClick('/CurrentVideoView')">
              <el-icon><icon-menu /></el-icon>
              <span>视频展示</span>
            </el-menu-item>
          </el-menu-item-group>
        </el-sub-menu>
      </el-menu>
    </el-col>
    <el-col :span="20">
      <component :is="tabsCurrent"></component>
    </el-col>
  </el-row>
</template>

<script setup lang="ts">
import {
  Menu as IconMenu,
  Setting,
} from '@element-plus/icons-vue'
import {ref, onMounted} from 'vue'
import {useRouter} from 'vue-router'
import { userCodeStore } from '../../stores/userCodeStore'
import PlayerView from '@/views/baseview/PlayerView.vue'
import TeamView from '@/views/baseview/TeamView.vue'
import CurrentMatchView from '@/views/baseview/MatchView.vue'
import LegendView from '@/views/baseview/LegendView.vue'
import VideoView from '@/views/baseview/VideoView.vue'
import MatchPlayerCommentView from '@/views/baseview/MatchPlayerCommentView.vue'

const userCode=userCodeStore();
const router=useRouter()

const tabsCurrent=ref(PlayerView)

const handleOpen = (key: string, keyPath: string[]) => {
  console.log(key, keyPath)
}
const handleClose = (key: string, keyPath: string[]) => {
  console.log(key, keyPath)
}
// eslint-disable-next-line @typescript-eslint/no-explicit-any
const menuItemClick=(item:any)=>{
  tabsCurrent.value=item;
}
const menuItemOutClick=(item:string)=>{
  router.push({ path: item})
}

onMounted(()=>{
  if(userCode.codeVerify==false){
    router.push("login");
  }
});

</script>

<style scoped >

.demo-form-inline .el-select {
  --el-select-width: 220px;
}
</style>
