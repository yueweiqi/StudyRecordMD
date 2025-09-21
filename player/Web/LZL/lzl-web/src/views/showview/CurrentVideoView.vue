<template>
  <div class="h-100 w-100 bg_div position-relative">
    <div class="w-100 ">
      <el-row class="w-100 text-light" style="padding-top: 5rem !important">
        <el-col :span="2"></el-col>
        <el-col :span="20">
          <el-row style="padding-top: 4rem !important">
              <el-col :span="24" class="text-center fs-2">
                  <span>{{ currentMatchData.currentMatch.name }}</span>
              </el-col>
          </el-row>
          
        </el-col>
        <el-col :span="2"></el-col>
      </el-row>
      <el-row class="w-100 d-flex justify-content-center video-div-base" style="padding-top: 3rem !important">
        <el-col class="h-100" :span="24">
                <video loop
                autoplay
                class="h-100 w-100" 
                ref="videoPlayer"
                :src="videoUrl"
                controls="false"
                preload="metadata">
                您的浏览器不支持HTML5视频播放
            </video>
        </el-col>
      </el-row>
    </div>
    <div class="position-absolute" style="top: 3rem;left: 3rem;">
      <CountDownTime
        title="距离比赛开始"
        prefix=""
        suffix=""
        finishText="比赛马上开始"
        :duration="timeDuration"
        format="HH:mm:ss"
        :autoStart="true"
      />
    </div>
    <div class="position-absolute" style="top: 3rem;left: 16rem;">

      <div class="div_score">

      </div>
      <div class="text-light div_score-title-postion">
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
  const mTime=new Date(currentVideoData.startTime);
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
const videoUrl= computed(() => {
    const dddd=fileBasePath.value+currentVideoData.videoUrl;
    console.log(dddd);
    return dddd;
})
  


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
const currentVideoData=reactive({
    id:"",
    startTime:"",
    videoUrl:"",
    state:0
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
   http.get("/Video/CurrentTeamInfo")
  .then((res)=>{
      console.log(res.data.data);
      const resData=res.data.data;
      currentVideoData.id=resData.id;
      currentVideoData.startTime=resData.startTime;
      currentVideoData.videoUrl=resData.videoUrl;
      currentVideoData.state=resData.state;
   });
});
</script>

<style scoped>
/* //全屏按钮 */
video::-webkit-media-controls-fullscreen-button {
    display: none;
}
/* //播放按钮 */
video::-webkit-media-controls-play-button {
    display: none;
}
/* //进度条 */
video::-webkit-media-controls-timeline {
    display: none;
}
/* //观看的当前时间 */
video::-webkit-media-controls-current-time-display{
    display: none;            
}
/* //剩余时间 */
video::-webkit-media-controls-time-remaining-display {
    display: none;            
}
/* //音量按钮 */
video::-webkit-media-controls-mute-button {
    display: none;            
}
video::-webkit-media-controls-toggle-closed-captions-button {
    display: none;            
}
/* //音量的控制条 */
video::-webkit-media-controls-volume-slider {
    display: none;            
}
/* //所有控件 */
video::-webkit-media-controls-enclosure{ 
    display: none;
}
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
  padding: 2rem;
  border-radius: 0.8rem;
  background-color:#9e9e9e57 !important;
  margin: 0 auto;
  height:7rem;
  width:10rem;
}
.div_score-title {
  font-size: 1.2rem;
  margin-bottom: 0.1rem;
  /* font-weight: bold; */
}
.div_score-display {
  /* font-weight: bold; */
  font-size: 2rem;
  font-family: monospace;
  margin: 0.1rem 0;
}
.div_score-title-postion{
  position: relative;top: -6rem;
}
.video-div-base {
  height: 80vh;
}
</style>
