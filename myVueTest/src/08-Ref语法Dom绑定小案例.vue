<template>
  <div class="box">
    <input type="text" ref="customInputRef" v-model="customInputModel">
  </div>
</template>

<script setup>
import {ref,onMounted} from 'vue'

const customInputModel=ref("")

const customInputRef=ref(null);

//组件挂载完成后，也就是页面渲染完成后
onMounted(()=>{
  if(customInputRef.value){
    customInputRef.value.focus();
    customInputModel.value="焦点聚焦";
  }
})
</script>

<style scoped>

</style>