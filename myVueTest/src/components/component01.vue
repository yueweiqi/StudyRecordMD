<template>
    <div>
        <h3>{{ localMsg }}</h3>
        <button @click="send">发送消息 </button>
        <button @click="updateFatherProp">修改父组件传过来的参数 </button>
    </div>
</template>

<script setup>
import {ref} from "vue"
const props=defineProps({
    msg:{
        type:String,
        require:true
    }
})
const localMsg=ref(props.msg);
const updateFatherProp=()=>{
    localMsg.value="12312312";
}
const emits=defineEmits(["sendMessage"])
const send=()=>{
    emits("sendMessage",props.msg)
}
</script>

<style lang="scss" scoped>

</style>