import { defineStore } from 'pinia'
import {ref} from 'vue'

export const userCodeStore = defineStore('code', ()=>{

  const code=ref('');
  const codeVerify=ref(false);
  const verifySuccess=(c:string)=>{
    code.value=c;
    codeVerify.value=true;
  }
  return {code,codeVerify,verifySuccess}
},{
  //持久化存储 sessionStorage localStorage
  persist:{
    storage: sessionStorage
  }
})
