 <!-- vbase-3 快捷插件语法-->
<script setup>
import {ref,onBeforeMount,onMounted,onBeforeUpdate,onUpdated} from 'vue';
const count=ref(0);
const addcount=()=>{
  count.value++;
}
//beforeCreate：1.组件初始化之前；2.created：组件初始化完成,已经包含在setup中
onBeforeMount(()=>{
  console.log("组件挂载前:onBeforeMount函数执行");
})
//这两个方法中间在执行组件渲染，可能就是生成html
onMounted(()=>{
  console.log("组件挂载前:onMounted函数执行");
})
onBeforeUpdate(()=>{
  console.log("组件更改前:onBeforeUpdate函数执行");
})
//这两个方法中间在执行更改的组件渲染，可能就是生成html
onUpdated(()=>{
  console.log("组件更改后:onUpdated函数执行")
})
</script>
<template>
  <div class="box">
    <button class="my-button" @click="addcount">Result:{{ count }}</button>
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