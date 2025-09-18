<template>
 <el-dialog v-model="addForm.visible" :title="addForm.aOrUText">
    <el-form :model="addForm">
      <el-form-item label="选手姓名" :label-width="addForm.labelWidth">
        <el-input v-model="addFormData.name" autocomplete="off" />
      </el-form-item>
      <el-form-item label="队伍名称" :label-width="addForm.labelWidth">
      <el-select
                v-model="addFormData.teamId"
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
      <el-form-item label="选手位置" :label-width="addForm.labelWidth">

        <el-radio-group v-model="addFormData.position">
            <el-radio v-for="item in addFormInit.dataPositionEnumDtos.slice(0,5)"
                  :key="item.id"
                  :value="item.id"
                  :label="item.description"
                  size="large"
            />
            <p></p>
            <el-radio v-for="item in addFormInit.dataPositionEnumDtos.slice(5,100)"
                  :key="item.id"
                  :value="item.id"
                  :label="item.description"
                  size="large"
            />
        </el-radio-group>
      </el-form-item>
      <el-form-item label="Rank分段" :label-width="addForm.labelWidth">
        <el-radio-group v-model="addFormData.rankName">
            <el-radio v-for="item in addFormInit.dataRankNameEnumDtos"
                  :key="item.id"
                  :value="item.id"
                  :label="item.description"
                  size="large"
            />
        </el-radio-group>
      </el-form-item>
      <el-form-item label="Rank分数" :label-width="addForm.labelWidth">
        <el-input v-model="addFormData.rankScore" autocomplete="off" />
      </el-form-item>

      <el-form-item label="学校名称" :label-width="addForm.labelWidth">
        <el-input v-model="addFormData.school" autocomplete="off" />
      </el-form-item>
      <el-form-item label="擅长英雄" :label-width="addForm.labelWidth">
        <el-input v-model="addFormData.skilledHeros" autocomplete="off" />
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
    <el-form-item label="选手名称">
      <el-input v-model="tablePage.name" />
    </el-form-item>
    <el-form-item label="队伍名称">
      <el-select
                v-model="tablePage.teamId"
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
            <el-table-column prop="name" label="选手名称" />
            <el-table-column prop="team[0].name" label="队伍名称" />
            <el-table-column prop="positionStr" label="选手位置" />
            <el-table-column prop="rankNameStr" label="分段" />
            <el-table-column prop="rankScore" label="分数" />
            <el-table-column prop="school" label="学校" />
            <el-table-column  label="擅长英雄" >
              <template #default="scope">
                {{ scope.row.skilledHeros.join(",") }}
               </template>
            </el-table-column>
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
  http.post("/Player/Delete",{
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
  addFormData.teamId=row.teamId;
  addFormData.position=row.position;
  addFormData.rankScore=row.rankScore;
  addFormData.rankName=row.rankName;
  addFormData.school=row.school;
  addFormData.skilledHeros=row.skilledHeros.join(',');
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
interface DataEnumDto{
  id:number;
  name:string;
  description:string;
}
const addFormInit=reactive<{
  dataPositionEnumDtos:DataEnumDto[];
  dataRankNameEnumDtos:DataEnumDto[];
}>({
  dataPositionEnumDtos:[],
  dataRankNameEnumDtos:[]
});

const onaddFromClick=()=>{
  if(addForm.addOrUpdate==1){
    http.post("/Player/Add",addFormData).then((res)=>{
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
    http.post("/Player/Update",addFormData).then((res)=>{
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
  teamId:"",
  position:1,
  avater:"",
  rankScore:0,
  rankName:0,
  school:"",
  skilledHeros:""
});
//#endregion

//#region 表格

interface Player {
  name: string;
  positionStr:string;
  avater:string;
  rankScore:number;
  rankNameStr:string;
  school:string;
  skilledHeros:string[];
  team:[];
}
const tableData = reactive<Player[]>([]);
const tablePage=reactive({
    pageSize:20,
    pageCount:20,
    pageIndex:1,
    teamId:"",
    name:"",
});
const pageUpdate=(pageIndex: number)=>{
    console.log(pageIndex);
    pageDataChange();
}
const pageDataChange=()=>{
  http.post("/Player/GetPageData",tablePage)
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
   http.get("/Player/GetAddInit").then((res)=>{
      console.log(res);
      addFormInit.dataPositionEnumDtos.splice(0, addFormInit.dataPositionEnumDtos.length, ...res.data.data.dataPositionEnumDtos);
      addFormInit.dataRankNameEnumDtos.splice(0, addFormInit.dataRankNameEnumDtos.length, ...res.data.data.dataRankNameEnumDtos);
   });
   pageDataChange();
});
//#endregion
</script>

<style scoped>

</style>
