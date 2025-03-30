<script setup>
import ComponentSon from "./componentson.vue"
</script>

<template>
  <div class="father">
    <h2>父组件</h2>

    <ComponentSon></ComponentSon>
  </div>
</template>

<style scoped>
.father {
  padding: 114px;
  border: 1px solid greenyellow;
}
</style>