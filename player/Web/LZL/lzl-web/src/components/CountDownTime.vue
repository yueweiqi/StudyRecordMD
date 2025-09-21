
<template>
  <div class="countdown-container">

  </div>
  <div class="text-light" style="position: relative;top: -6rem;">
    <div v-if="title" class="countdown-title text-center" :style="titleStyle">{{ title }}</div>
    <div class="countdown-display text-center">
      <span v-if="prefix" class="countdown-prefix">{{ prefix }}</span>
      <span class="countdown-value" >{{ formattedTime }}</span>
      <span v-if="suffix" class="countdown-suffix">{{ suffix }}</span>
    </div>
    <div class="countdown-controls text-center" v-show="false">
      <button @click="toggle" :disabled="!remainingTime">
        {{ isActive ? '暂停' : '继续' }}
      </button>
      <button @click="reset">重置</button>
    </div>
    <div v-if="!remainingTime && finishText" class="countdown-finish text-center">
      {{ finishText }}
    </div>
  </div>
</template>

<script setup lang="ts">
import { ref, computed, onMounted, onUnmounted, watch } from 'vue'

interface Props {
  title?: string
  titleStyle?: Record<string, string>
  prefix?: string
  suffix?: string
  finishText?: string
  duration: number // 单位：秒
  format?: string // 默认: 'HH:mm:ss'
  autoStart?: boolean
}

const props = withDefaults(defineProps<Props>(), {
  format: 'HH:mm:ss',
  autoStart: true
})

const remainingTime = ref(props.duration)
const isActive = ref(props.autoStart)
let timer: number | null = null

const formattedTime = computed(() => {
  const hours = Math.floor(remainingTime.value / 3600)
  const minutes = Math.floor((remainingTime.value % 3600) / 60)
  const seconds = remainingTime.value % 60

  return props.format
    .replace('HH', hours.toString().padStart(2, '0'))
    .replace('H', hours.toString())
    .replace('mm', minutes.toString().padStart(2, '0'))
    .replace('m', minutes.toString())
    .replace('ss', seconds.toString().padStart(2, '0'))
    .replace('s', seconds.toString())
})

const start = () => {
  if (timer) clearInterval(timer)
  isActive.value = true
  timer = setInterval(() => {
    if (remainingTime.value > 0) {
      remainingTime.value--
    } else {
      clearInterval(timer as number)
      timer = null
      isActive.value = false
    }
  }, 1000)
}

const pause = () => {
  if (timer) {
    clearInterval(timer)
    timer = null
  }
  isActive.value = false
}

const toggle = () => {
  if (isActive.value) {
    pause()
  } else {
    start()
  }
}

const reset = () => {
  pause()
  remainingTime.value = props.duration
  if (props.autoStart) {
    start()
  }
}

watch(() => props.duration, (newVal) => {
  remainingTime.value = newVal
  if (isActive.value) {
    start()
  }
})

onMounted(() => {
  if (props.autoStart) {
    start()
  }
})

onUnmounted(() => {
  if (timer) {
    clearInterval(timer)
  }
})
</script>

<style scoped>
.countdown-container {
  font-family: Arial, sans-serif;
  text-align: center;
  padding: 2rem;
  border-radius: 0.8rem;
  background-color:#9e9e9e57 !important;
  margin: 0 auto;
  height:7rem;
  width:12rem;
}

.countdown-title {
  font-size: 1.2rem;
  margin-bottom: 0.1rem;
  /* font-weight: bold; */
}

.countdown-display {
  font-size: 2rem;
  font-family: monospace;
  margin: 0.1rem 0;
}

.countdown-value {
  font-weight: bold;
  /*color: #2c4950;*/
}

.countdown-controls {
  margin-top: 0.1rem;
}

button {
  padding: 0.7rem 2rem;
  margin: 0 0.5rem;
  border: none;
  border-radius: 0.5rem;
  background-color: #42b983;
  color: white;
  cursor: pointer;
  transition: background-color 0.3s;
}

button:hover {
  background-color: #3aa876;
}

button:disabled {
  background-color: #cccccc;
  cursor: not-allowed;
}

.countdown-finish {
  margin-top: 1rem;
  font-weight: bold;
  color: #e74c3c;
}
</style>
