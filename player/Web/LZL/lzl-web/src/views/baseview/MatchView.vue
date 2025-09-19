<template>
   <el-dialog v-model="addForm.visible" :title="addForm.aOrUText">
     <el-form :model="addForm">
       <el-form-item label="比赛名称" :label-width="addForm.labelWidth">
         <el-input v-model="addFormData.name" autocomplete="off" />
       </el-form-item>
       <el-form-item label="蓝方队伍" :label-width="addForm.labelWidth">
       <el-select
                 v-model="addFormData.blueId"
                 placeholder="查询所有"
                 clearable
                 filterable
               >
                 <el-option v-for="item in teamList"
                   :key="item.id"
                   :value="item.id"
                   :label="item.name"
                 />
               </el-select>
     </el-form-item>

       <el-form-item label="蓝方比分" :label-width="addForm.labelWidth">
         <el-input v-model="addFormData.blueScore" autocomplete="off" />
       </el-form-item>

       <el-form-item label="红方队伍" :label-width="addForm.labelWidth">
       <el-select
                 v-model="addFormData.redId"
                 placeholder="查询所有"
                 clearable
                 filterable
               >
                 <el-option v-for="item in teamList"
                   :key="item.id"
                   :value="item.id"
                   :label="item.name"
                 />
               </el-select>
     </el-form-item>

       <el-form-item label="红方比分" :label-width="addForm.labelWidth">
         <el-input v-model="addFormData.redScore" autocomplete="off" />
       </el-form-item>
       <el-form-item label="比赛时间" :label-width="addForm.labelWidth">
      <el-col :span="6">
        <el-date-picker
        v-model="addFormData.startTime"
        type="datetime"
        placeholder="Select date and time"
        format="YYYY/MM/DD HH:mm:ss"
        value-format="YYYY-MM-DD HH:mm:ss"
      />
      </el-col>
      <el-col :span="2" class="text-center">
        <span class="text-gray-500">-</span>
      </el-col>
      <el-col :span="11">
        <el-date-picker
        v-model="addFormData.endTime"
        type="datetime"
        placeholder="Select date and time"
        format="YYYY/MM/DD HH:mm:ss"
        value-format="YYYY-MM-DD HH:mm:ss"
      />
      </el-col>
    </el-form-item>

     </el-form>
     <template #footer>
       <span class="dialog-footer">
         <el-button @click="addForm.visible = false">取消</el-button>
         <el-button type="primary" @click="onaddFromClick">
           {{addForm.aOrUText}}
         </el-button>
       </span>
     </template>
   </el-dialog>


   <el-row class="pt-5">
     <el-col :span="12">
       <el-form :model="tablePage" label-width="120px">
     <el-form-item label="比赛名称">
       <el-input v-model="tablePage.name" />
     </el-form-item>
     <el-form-item label="蓝色队伍">
       <el-select
                 v-model="tablePage.blueId"
                 placeholder="查询所有"
                 clearable
                 filterable
               >
                 <el-option v-for="item in teamList"
                   :key="item.id"
                   :value="item.id"
                   :label="item.name"
                 />
               </el-select>
     </el-form-item>
     <el-form-item label="红色队伍">
       <el-select
                 v-model="tablePage.redId"
                 placeholder="查询所有"
                 clearable
                 filterable
               >
                 <el-option v-for="item in teamList"
                   :key="item.id"
                   :value="item.id"
                   :label="item.name"
                 />
               </el-select>
     </el-form-item>
     <el-form-item>
       <el-button type="primary" @click="onSubmit">查找</el-button>

       <el-button type="primary" @click="onAddClick">添加</el-button>
     </el-form-item>
   </el-form>
     </el-col>
   </el-row>

   <el-row class="p-3">
         <el-col :span="24">
           <el-table :data="tableData" style="width: 100%">
            <el-table-column prop="name" label="比赛状况" >
              <template #default="scope">
                <el-button v-if="scope.row.state==1" type="success" size="small" @click="onUpdateProgressClick(scope.row.id,0)"
                   >正在比赛</el-button
                 >
                 <el-button plain v-else type="warning" size="small" @click="onUpdateProgressClick(scope.row.id,1)"
                   >点击开始</el-button
                 >
              </template>
              </el-table-column>

             <el-table-column prop="name" label="比赛名称" />
             <el-table-column prop="blueTeam[0].name" label="蓝方队伍" />
             <el-table-column prop="blueScore" label="蓝方比分" />
             <el-table-column prop="redTeam[0].name" label="红方队伍" />
             <el-table-column prop="redScore" label="红方比分" />
             <el-table-column prop="startTimeStr" label="开始时间" />
             <el-table-column fixed="right" label="操作" width="250">
               <template #default="scope">



                 <el-button  type="primary" size="small" @click="onUpdateClick(scope.row)"
                   >修改</el-button
                 >
                 <el-button  type="danger" size="small" @click="onDeleteClick(scope.row)"
                 >删除</el-button>
               </template>
             </el-table-column>
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
 import { ref,onMounted, reactive } from 'vue'
 import http from '@/utils/http'
 import {useRouter} from 'vue-router'
 import { userCodeStore } from '@/stores/userCodeStore'
 import { ElMessage } from 'element-plus'

 const userCode=userCodeStore();
 const router=useRouter()
 const onSubmit=()=>{
   pageDataChange();
 }
 const teamList=reactive([
   {
     id: 0,
     name:'',
   }
 ]);

 //#region 删除
 const onDeleteClick=(row)=>{
   http.post("/Match/Delete",{
     id:row.id
   }).then((res)=>{
       console.log(res);
       ElMessage({
         showClose: true,
         message: '删除成功',
         type: 'success',
       })
       pageDataChange();
    });
 }
 //#endregion

 //#region 修改
 const onUpdateClick=(row)=>{

   addFormData.id=row.id;
   addFormData.name=row.name;
   addFormData.blueId=row.blueId;
   addFormData.blueScore=row.blueScore;
   addFormData.redId=row.redId;
   addFormData.redScore=row.redScore;
   addFormData.startTime=row.startTime;
   addFormData.endTime=row.endTime;

   addForm.addOrUpdate=2;
   addForm.aOrUText="修改";
   addForm.visible=true;

 }
 //#endregion

 //#region 添加
 const onAddClick=()=>{
   addForm.addOrUpdate=1;
   addForm.visible=true;
   addForm.aOrUText="添加";
 }

 const onaddFromClick=()=>{
   if(addForm.addOrUpdate==1){
     http.post("/Match/Add",addFormData).then((res)=>{
       console.log(res);
       ElMessage({
         showClose: true,
         message: '添加成功',
         type: 'success',
       })
       pageDataChange();
    });
   }
   if(addForm.addOrUpdate==2){
     http.post("/Match/Update",addFormData).then((res)=>{
       console.log(res);
       ElMessage({
         showClose: true,
         message: '修改成功',
         type: 'success',
       })
       pageDataChange();
    });
   }
 }
 // addOrUpdate=1 添加 addOrUpdate=2 修改
 const addForm=reactive({
     labelWidth:"120px",
     visible:false,
     addOrUpdate:1,
     aOrUText:"添加"
 });
 const addFormData=reactive({
   id:"",
   name:"",
   blueId:"",
   blueScore:0,
   redId:"",
   redScore:0,
   startTime:'',
   endTime:''
 });
 //#endregion

 //#region 表格

 interface Player {
   id:string;
   name: string;
   blueId:string;
   blueScore:number;
   blueTeam:[];
   redId:string;
   redScore:number;
   redTeam:[];
   state:0,
   startTimeStr:""
 }
 const tableData = reactive<Player[]>([]);
 const tablePage=reactive({
     pageSize:20,
     pageCount:20,
     pageIndex:1,
     blueId:"",
     redId:"",
     name:"",
 });
 const pageUpdate=(pageIndex: number)=>{
     console.log(pageIndex);
     pageDataChange();
 }
 const onUpdateProgressClick=(id,state)=>{
    http.post("/Match/UpdateProgressState",{
      id:id,
      state:state
    })
    .then((res)=>{
        console.log(res);
        pageDataChange();
      });
 }
 const pageDataChange=()=>{
   http.post("/Match/GetPageData",tablePage)
   .then((res)=>{
       console.log(res.data.data.items);
       tableData.splice(0, tableData.length, ...res.data.data.items);
       tablePage.pageCount=res.data.data.totalCount;
       console.log(res);
    });
 }
 //#endregion

 //#region 初始化
 onMounted(()=>{
   if(userCode.codeVerify==false){
     router.push("login");
   }
    http.get("/Team/GetAll").then((res)=>{
       console.log(res);
       teamList.splice(0, teamList.length, ...res.data.data);
    });
    pageDataChange();
 });
 //#endregion
 </script>

 <style scoped>

 </style>
