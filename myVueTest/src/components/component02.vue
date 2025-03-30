<template>
    <div>
        <h4>Result:{{ count }}</h4>
    </div>
</template>

<script setup>
import {ref} from "vue"
const count=ref(0);

const addCount=(num)=>{
    count.value=count.value+num;
}
//暴露子组件方法或者变量，父组件可以通过ref绑定的方式进行调用
defineExpose({
    addCount
})
</script>

<style scoped>

</style>