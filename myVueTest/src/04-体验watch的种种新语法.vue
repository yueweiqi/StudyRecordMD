<script setup>
import {ref,reactive,computed, watch} from 'vue';
const count=reactive({
  count:1
});
const addCount=()=>
{
  count.count++;
}
const newCount=computed(()=>{
  return count.count*2;  
})


//深度克隆解决对象深度监听，解决新值旧值一样的问题
let oldCountState=JSON.parse(JSON.stringify(count));
const oldCountString=ref(JSON.stringify(oldCountState));
watch(count,(newvalue)=>{
  oldCountString.value=JSON.stringify(oldCountState);
  oldCountState=JSON.parse(JSON.stringify(newvalue));
  
},{
  deep: true,//深度监听
  immediate: true//立即监听，初始化时执行一次
});

</script>

<template>
  <div class="box">
      <button class="my-button" @click="addCount">Result : {{ count.count }}</button>
      <p>Computed== {{ newCount }}</p>

      <p>Watch==New {{ count.count }},Old {{ oldCountString }}</p>
  </div>

</template>

<style scoped>
.box {
  /*display: flex;*/
  justify-content: center;
  align-items: center;
  height: 100vh;
  padding: 20px;
}

.my-button {
  display: inline-block;
  text-align: center;
  vertical-align: middle;
  padding: 15px 32px;
  text-decoration: none;
  font-size: 16px;
  font-weight: 500;
  border-radius: 4px;
  border: none;
  cursor: pointer;

  background-color: #4caf50;
  color: white;

  transition: background-color 0.3s ease;
  &:hover {
    background-color: #45a049;
  }

  &:active {
    background-color: #388e3c;
  }
}
</style>
