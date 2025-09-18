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
        <el-menu-item index="1" @click="menuItemClick(PlayerView)">
          <el-icon><icon-menu /></el-icon>
          <span>选手管理</span>
        </el-menu-item>
        <el-menu-item index="2" @click="menuItemClick(TeamView)">
          <el-icon><icon-menu /></el-icon>
          <span>队伍管理</span>
        </el-menu-item>
        <el-menu-item index="3" @click="menuItemClick(CurrentMatchView)">
          <el-icon><icon-menu /></el-icon>
          <span>比赛管理</span>
        </el-menu-item>
        <el-menu-item index="4" @click="menuItemOutClick('/CurrentMatchView')">
          <el-icon><icon-menu /></el-icon>
          <span>选手信息展示</span>
        </el-menu-item>
        <el-menu-item index="5" v-show="false">
          <el-icon><setting /></el-icon>
          <span>设置</span>
        </el-menu-item>
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
