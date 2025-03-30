<template>
    <div class="son">
        <h2>孙子组件</h2>
        <h1>祖先组件传递数据:{{ msg }}</h1>
        <button @click="updateBase1">修改祖先1</button>
        <button @click="updateBase2">修改祖先2</button>
    </div>
</template>

<script setup>
import { inject } from "vue";

let num1=10;
const msg=inject("provideBaseMsg");
const updateBase1=()=>{
    num1=num1+10;
    msg.value=msg.value+num1;
}


const updateBaseInt=inject("provideUpdateBaseInt");
let num=10;
const updateBase2=()=>{
    num=num+20;
    updateBaseInt(num)
}

</script>

<style scoped>
.son {
  padding: 51px;
  border: 1px solid pink;
}
</style>