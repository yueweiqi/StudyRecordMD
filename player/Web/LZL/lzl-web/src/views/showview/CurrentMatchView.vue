<template>
  <div class="h-100 w-100 bg_div">
    <div class="w-100 d-flex justify-content-center ">
      <el-row class="w-100 text-light" style="padding-top: 10rem !important">
        <el-col :span="2"></el-col>
        <el-col :span="20">
          <el-row>
              <el-col :span="24" class="text-center fs-2">
                  <span>{{ currentMatchData.currentMatch.name }}</span>
              </el-col>
          </el-row>
          <el-row class="">
              <el-col :span="3"></el-col>
              <el-col :span="1">
                <img style="height:3rem;" :src="fileBasePath+currentMatchData.currentMatch.blueTeam[0].avater" />
              </el-col>
              <el-col :span="6" class="text-left fs-2">
                  <span>{{ currentMatchData.currentMatch.blueTeam[0].name }}</span>
              </el-col>
              <el-col :span="4" class="text-center fs-2">
                <div class="d-flex">
                  <div class="p-2 flex-fill">{{ currentMatchData.currentMatch.blueScore }}</div>
                  <div class="p-2 flex-fill">:</div>
                  <div class="p-2 flex-fill">{{ currentMatchData.currentMatch.redScore }}</div>
                </div>
              </el-col>
              <el-col :span="3"></el-col>
              <el-col :span="1">
                <img style="height:3rem;" :src="fileBasePath+currentMatchData.currentMatch.redTeam[0].avater" />
              </el-col>
              <el-col :span="6" class="text-left fs-2">
                  <span>{{ currentMatchData.currentMatch.redTeam[0].name }}</span>
              </el-col>
          </el-row>
          <el-row class="pt-4 text-left" v-for="(item, index) in currentMatchData.bluePlayerList" :key="item.id">
            <el-col :span="2">
              {{ item.name }}
            </el-col>
            <el-col :span="2">
              {{ item.rankNameStr }}
            </el-col>
            <el-col :span="1">
              {{ item.rankScore }}
            </el-col>
            <el-col :span="2">
              {{ item.school }}
            </el-col>
            <el-col :span="3">
              {{ item.skilledHeros.join(",") }}
            </el-col>

            <el-col :span="4">

            </el-col>
            <el-col :span="2">
              {{ currentMatchData.redPlayerList[index].name }}
            </el-col>
            <el-col :span="2">
              {{ currentMatchData.redPlayerList[index].rankNameStr }}
            </el-col>
            <el-col :span="1">
              {{ currentMatchData.redPlayerList[index].rankScore }}
            </el-col>
            <el-col :span="2">
              {{ currentMatchData.redPlayerList[index].school }}
            </el-col>
            <el-col :span="3">
              {{ currentMatchData.redPlayerList[index].skilledHeros.join(",") }}
            </el-col>

          </el-row>
        </el-col>
        <el-col :span="2"></el-col>
      </el-row>
    </div>

  </div>
</template>

<script setup lang="ts">
import { ref,onMounted, reactive } from 'vue'
import http from '@/utils/http'

const fileBasePath = ref(import.meta.env.VITE_File_BASE_URL);
const currentMatchData=reactive({
  currentMatch:{
    name:"",
    blueTeam:[
      {name:"",avater:""}
    ],
    blueScore:0,
    redTeam:[
      {name:"",avater:""}
    ],
    redScore:0
  },
  bluePlayerList:[],
  redPlayerList:[]
});
onMounted(()=>{
  http.get("/Match/CurrentTeamInfo")
  .then((res)=>{
      console.log(res.data.data);
      const resData=res.data.data;
      currentMatchData.currentMatch=resData.currentMatch;
      currentMatchData.bluePlayerList=resData.bluePlayerList;
      currentMatchData.redPlayerList=resData.redPlayerList;
   });
});
</script>

<style scoped>
.bg_div{
  background-image: url('@/assets/showview/bg.jpg');
  background-size: cover; /* 或其他你需要的背景大小设置 */
  background-position: center; /* 或其他你需要的背景位置设置 */
}
</style>
