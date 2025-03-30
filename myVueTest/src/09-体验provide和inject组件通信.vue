<script setup>
import ComponentFather from "./components/componentfather.vue"
import { ref,provide } from "vue";
const buttonClass=ref("my-button");
let i=0;
const baseMsg=ref("哈哈哈哈哈，我是祖先组件传递过来的")
provide("provideBaseMsg",baseMsg);
const provideBaseMsg=()=>{
  baseMsg.value="我修改了"+i++;
}


const baseInt=ref(0);
const updateBaseInt=(num)=>{
  baseInt.value=num;
}
provide("provideUpdateBaseInt",updateBaseInt);


</script>

<template>
  <div class="box">
    <h4>{{ baseInt }}</h4>
    <h2>祖先组件</h2>
    <button @click="provideBaseMsg" v-bind:class="buttonClass"></button>
    <ComponentFather></ComponentFather>
  </div>
</template>

<style scoped>
.box {
  display: flex;
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