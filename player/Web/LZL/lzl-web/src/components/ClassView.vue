<template>
  <el-row v-show="true" class="p-3">
        <el-col :span="24">
          <el-form :inline="true" class="demo-form-inline">

            <el-form-item label="园所名称">
              <el-select
                v-model="organId"
                placeholder="查询所有"
                clearable
                filterable
              >
                <el-option v-for="item in organList"
                  :key="item.id"
                  :value="item.id"
                  :label="item.name"
                />
              </el-select>
            </el-form-item>

            <el-form-item>
              <el-button type="primary" @click="organSelect" >查询</el-button>
            </el-form-item>
            </el-form>
        </el-col>

      </el-row>
      <el-row class="p-3">
        <el-col :span="24">
          <el-table :data="tableData" style="width: 100%">
            <el-table-column prop="organName" label="机构名称" />
            <el-table-column prop="className" label="班级名称" />
            <el-table-column prop="year" label="年"  />
            <el-table-column prop="month" label="月" />
            <el-table-column prop="count" label="总数" />
          </el-table>
          <el-pagination background
          layout="total,prev, pager, next"
          :total="tablePage.pageCount"
          :page-size="tablePage.pageSize"
          v-model:current-page="tablePage.pageIndex"
          class="mt-2"
          @current-change="tablePage.pageIndex"
          @update:current-page="pageUpdate"/>
        </el-col>
      </el-row>
</template>

<script setup lang="ts">
import {ref,reactive, onMounted} from 'vue'
import http from '../utils/http'
import {useRouter} from 'vue-router'
import { userCodeStore } from '../stores/userCodeStore'

const userCode=userCodeStore();
const router=useRouter()
const organSelect=()=>{
  tablePage.pageIndex=1;
  pageDataChange();
}
const organId=ref<number|string>('');
const organList=reactive([
  {
    id: 0,
    name:'',
  }
])

interface SummaryEntity {
  organName: string;
  className:string;
  year: number;
  month:number;
  count:number;
}
const tableData = reactive<SummaryEntity[]>([]);

const tablePage=reactive({
    pageSize:20,
    pageCount:20,
    pageIndex:1,
    pageCurrentChange:()=>{
        console.log(tablePage);
        tablePage.pageCount+=100;
    },
});

const pageUpdate=(pageIndex: number)=>{
    console.log(pageIndex);
    pageDataChange();
}
const pageDataChange=()=>{
  let aorganId:unknown=0;
  if(organId.value!=''){
    aorganId=organId.value;
  }
  http.post("/ExpressionSummary/GetExpressionSummaryPrivateClassList",{
    PageSize:tablePage.pageSize,
    PageIndex:tablePage.pageIndex,
    OrganId:aorganId
   }).then((res)=>{
      console.log(res.data.data.items);
      tableData.splice(0, tableData.length, ...res.data.data.items);
      tablePage.pageCount=res.data.data.totalCount;
      console.log(res);
   });
}


onMounted(()=>{
  if(userCode.codeVerify==false){
    router.push("login");
  }
  pageDataChange();
   http.post("/ExpressionSummary/GetIdentityOrgan").then((res)=>{
      organList.splice(0, organList.length, ...res.data.result.items);
   });
});

</script>

<style scoped >

.demo-form-inline .el-select {
  --el-select-width: 220px;
}
</style>
