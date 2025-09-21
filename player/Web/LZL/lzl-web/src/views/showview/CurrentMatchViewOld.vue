<template>
  <div class="h-100 w-100 bg_div position-relative">
    <div class="w-100 d-flex justify-content-center ">
      <el-row class="w-100 text-light" style="padding-top: 5rem !important">
        <el-col :span="2"></el-col>
        <el-col :span="20">
          <el-row style="padding-top: 4rem !important">
              <el-col :span="24" class="text-center fs-2">
                  <span>{{ currentMatchData.currentMatch.name }}</span>
              </el-col>
          </el-row>
          <el-row style="padding-top: 4rem !important">
              <el-col :span="3"></el-col>
              <el-col :span="1" v-show="false">
                <el-avatar shape="square" size="large" :src="fileBasePath+currentMatchData.currentMatch.blueTeam[0].avatar" />
              </el-col>
              <el-col :span="6" class="text-left fs-1 ps-3">
                  <span>{{ currentMatchData.currentMatch.blueTeam[0].name }}</span>

              </el-col>
              <el-col :span="4" class="text-center fs-2">
                <div class="d-flex" v-if="false">
                  <div class="p-2 flex-fill">{{ currentMatchData.currentMatch.blueScore }}</div>
                  <div class="p-2 flex-fill">:</div>
                  <div class="p-2 flex-fill">{{ currentMatchData.currentMatch.redScore }}</div>
                </div>
              </el-col>
              <el-col :span="3"></el-col>
              <el-col :span="1" v-show="false">
                <el-avatar shape="square" size="large" :src="fileBasePath+currentMatchData.currentMatch.redTeam[0].avatar" />
              </el-col>
              <el-col :span="6" class="text-left fs-1 ps-3">
                  <span>{{ currentMatchData.currentMatch.redTeam[0].name }}</span>
              </el-col>
          </el-row>
          <el-row style="padding-top: 3rem !important">

          </el-row>
          <el-row class="fs-5 text-left mb-1" v-for="(item, index) in currentMatchData.bluePlayerList" :key="item.id">
            <el-col :span="11" class="col_player rounded-3" :class="{'col_player_height':item.height==1,'rounded-3':item.height==1}" >
              <el-row class="py-3 px-1 d-flex align-items-center">
                <el-col :span="6">
                  {{ item.name }}
                  <el-tag v-show="item.identity>0&&false" class="ml-2 ms-1" type="warning">{{ item.identityStr }}</el-tag>
                </el-col>
                <el-col :span="4">
                  {{ item.rankNameStr }}
                </el-col>
                <el-col :span="2">
                  {{ item.rankScore }}
                </el-col>
                <el-col :span="4">
                  {{ item.school }}
                </el-col>
                <el-col :span="8" v-show="false">
                  <el-avatar
                 class="ms-1"
                 v-for="item2 in item.skilledHeros"
                 :key="item2" shape="square" size="large"
                 :src="fileBasePath+item2.avatar" />
                </el-col>
              </el-row>
            </el-col>


            <el-col :span="2">

            </el-col>

            <el-col :span="11" class=" col_player rounded-3" :class="{'col_player_height':currentMatchData.redPlayerList[index].height==1,'rounded-3':currentMatchData.redPlayerList[index].height==1}">
              <el-row class="py-3 px-1 d-flex align-items-center">
                <el-col :span="6">
                  {{ currentMatchData.redPlayerList[index].name }}
                  <el-tag v-show="currentMatchData.redPlayerList[index].identity>0&&false" class="ml-2 ms-1" type="warning">{{ currentMatchData.redPlayerList[index].identityStr }}</el-tag>
                </el-col>
                <el-col :span="4">
                  {{ currentMatchData.redPlayerList[index].rankNameStr }}
                </el-col>
                <el-col :span="2">
                  {{ currentMatchData.redPlayerList[index].rankScore }}
                </el-col>
                <el-col :span="4">
                  {{ currentMatchData.redPlayerList[index].school }}
                </el-col>
                <el-col :span="8" v-show="false">
                  <el-avatar
                 class="ms-1"
                 v-for="item2 in currentMatchData.redPlayerList[index].skilledHeros"
                 :key="item2" shape="square" size="large"
                 :src="fileBasePath+item2.avatar" />
                </el-col>
              </el-row>
            </el-col>


          </el-row>
        </el-col>
        <el-col :span="2"></el-col>
      </el-row>
    </div>
    <div class="position-absolute" style="top: 3rem;left: 3rem;">
      <CountDownTime
        title="距离比赛开始"
        prefix=""
        suffix=""
        finishText="活动已结束"
        :duration="timeDuration"
        format="HH:mm:ss"
        :autoStart="true"
      />
    </div>
    <div class="position-absolute" style="top: 3rem;left: 16rem;">

      <div class="div_score">

      </div>
      <div class="text-light" style="position: relative;top: -5rem;">
        <div class="div_score-title text-center">
          <el-row>
            <el-col :span="10">
              {{ currentMatchData.currentMatch.blueTeam[0].name }}
            </el-col>
            <el-col :span="4">
                VS
            </el-col>
            <el-col :span="10">
              {{ currentMatchData.currentMatch.redTeam[0].name }}
            </el-col>
          </el-row>
        </div>
        <div class="div_score-display">
          <el-row>
            <el-col :span="10" class="text-end">
              {{ currentMatchData.currentMatch.blueScore }}
            </el-col>
            <el-col :span="4" class="text-center">
                :
            </el-col>
            <el-col :span="10" class="text-start">
              {{ currentMatchData.currentMatch.redScore }}
            </el-col>
          </el-row>
        </div>
      </div>

    </div>
  </div>
</template>

<script setup lang="ts">
import { ref,onMounted, reactive,computed } from 'vue'
import http from '@/utils/http'
import CountDownTime from '@/components/CountDownTime.vue';

const timeDuration = computed(() => {
  const mTime=new Date(currentMatchData.currentMatch.startTime);
  const nowTime=new Date();
  const timeDiff = mTime.getTime() - nowTime.getTime();
  console.log("后端时间"+mTime);
  console.log("前端时间"+nowTime);
  // 将毫秒转换为秒
  let secondsDiff:number = Math.ceil(timeDiff / 1000);
  console.log("相差秒数"+secondsDiff);
  if(secondsDiff<0)
      secondsDiff=0;
  return secondsDiff;
})

const fileBasePath = ref(import.meta.env.VITE_File_BASE_URL);
const currentMatchData=reactive({
  currentMatch:{
    name:"",
    startTime:"",
    blueTeam:[
      {name:"",avatar:"",identity:0,identityStr:""}
    ],
    blueScore:0,
    redTeam:[
      {name:"",avatar:"",identity:0,identityStr:""}
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
.col_player{
  /*background-color: #9e9e9e57;*/
  /* background-color: #f0f8ff1c; */
  background: linear-gradient(to right, #ffffff18,#ffffff00 );
}
.col_player_height{
  /* background-color: #9e9e9e57!important; */
  background: linear-gradient(to right, #fbff0238,#ffffff00 );
}
@keyframes gradientAnimation {
  0% { background-color: red; }
  100% { background-color: blue; }
}
.div_score{
  font-family: Arial, sans-serif;
  text-align: center;
  padding: 20px;
  border-radius: 8px;
  background-color:#9e9e9e57 !important;
  margin: 0 auto;
  height:6rem;
  width:10rem;
}
.div_score-title {
  font-size: 1.2rem;
  margin-bottom: 0.1rem;
  font-weight: bold;
}
.div_score-display {
  font-weight: bold;
  font-size: 2rem;
  font-family: monospace;
  margin: 0.1rem 0;
}
</style>
