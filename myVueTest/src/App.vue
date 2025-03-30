<template>
  <div class="box">
      <ComponentTest @send-message="reMessage" :msg="msg.Msg"></ComponentTest>
      
      <button v-bind:class="classButton" @click="updateMsg">修改</button>
  </div>
</template>

<script setup>
import { reactive, ref } from "vue";
import ComponentTest from "./components/component01.vue"
const msg=reactive({
  Msg:"这是子组件"
})
const updateMsg=()=>{
  msg.Msg="123121231";
}
const classButton=ref("my-button");
const reMessage=(msg)=>{
  alert(msg);
}
</script>

<style  scoped>
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