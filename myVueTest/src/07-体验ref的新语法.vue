<template>
  <div>
      <CompontentTest02 ref="CompontentTest2"></CompontentTest02>
      <button @click="comClick" class="my-button">测试Ref</button>
  </div>
</template>

<script setup>
import { onMounted,ref } from "vue";
import CompontentTest02 from "./components/component02.vue"

const CompontentTest2=ref(null);

const comClick=()=>{
  CompontentTest2.value.addCount(20);
}
</script>

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