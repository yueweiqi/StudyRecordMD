<template>
  <div>
      欢迎
  </div>
</template>

<script setup lang="ts">
import { onMounted } from 'vue';
import { userCodeStore } from '@/stores/userCodeStore'
import {useRouter} from 'vue-router'
import http from '@/utils/http'

const userCode=userCodeStore();
const router=useRouter()
onMounted(()=>{
  if(userCode.codeVerify==false){
    router.push("login");
  }

  http.post("/",{

  }).then(res=>{
    console.log(res);
  })
})


</script>

<style scoped>

</style>
