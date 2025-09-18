<template>
  <div class="h-100 w-100 d-flex align-items-center justify-content-center">
  <el-form
    ref="formRef"
    style="max-width: 600px"
    :model="codeForm"
    label-width="auto"
    class="demo-ruleForm"
  >
    <el-form-item
      label="请输入识别码"
      prop="code"
      :rules="[
        { required: true, message: '请输入识别码' },
        { type: 'number', message: 'code must be a number' },
      ]"
    >
      <el-input
        v-model.number="codeForm.code"
        type="text"
        autocomplete="off"
      />
    </el-form-item>
    <el-form-item>
      <el-button type="primary" @click="submitForm(formRef)">确认</el-button>
    </el-form-item>
  </el-form>
</div>
</template>

<script lang="ts" setup>
import {reactive, ref } from 'vue'
import type { FormInstance } from 'element-plus'
import {useRouter} from 'vue-router'
import { ElMessage } from 'element-plus'
import { userCodeStore } from '../../stores/userCodeStore'

const userCode=userCodeStore();

const router=useRouter()
const formRef = ref<FormInstance>()

const codeForm=reactive<{
  code:string
}>({
  code:''
})

const submitForm = (formEl: FormInstance | undefined) => {
  if (!formEl) return
  formEl.validate((valid) => {
    if (valid) {
      if(codeForm.code=='123456'){
        userCode.verifySuccess(codeForm.code);
        router.push('/');
      }else{
        ElMessage({
          message: '识别码错误',
          grouping: true,
          type: 'error',
        })
      }
    } else {
      console.log('error submit!')
    }
  })
}

</script>
