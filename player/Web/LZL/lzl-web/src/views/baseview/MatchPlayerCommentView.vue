<template>
  <el-dialog v-model="addForm.visible" :title="addForm.aOrUText">
    <el-form :model="addForm">


      <el-form-item label="比赛名称" :label-width="addForm.labelWidth">
      <el-select
                v-model="addFormData.match.id"
                placeholder="查询所有"
                clearable
                filterable
                @change="onChangeMatch"
                :disabled="addForm.addOrUpdate==2"
              >
                <el-option v-for="item in matchList"
                  :key="item.currentMatch.id"
                  :value="item.currentMatch.id"
                  :label="item.currentMatch.name"
                />
       </el-select>
    </el-form-item>
    <el-form-item label="小场名称" :label-width="addForm.labelWidth">
         <el-input v-model="addFormData.name" autocomplete="off" />
    </el-form-item>
    <el-form-item label="蓝方队伍" :label-width="addForm.labelWidth" style="color: deepskyblue;">
      <el-input v-model="addFormData.blueTeam.name" autocomplete="off" disabled/>
    </el-form-item>

    <el-form-item v-for="item in addFormData.bluePlayerList" :key="item" :label="item.positionStr+'-'+item.name" :label-width="addForm.labelWidth" style="color: deepskyblue;" >
      <el-input v-model="item.commentInfo" autocomplete="off"/>
    </el-form-item>

    <el-form-item label="红方队伍" :label-width="addForm.labelWidth" style="color: orangered;">
      <el-input v-model="addFormData.redTeam.name" autocomplete="off" disabled/>
    </el-form-item>

    <el-form-item v-for="item in addFormData.redPlayerList" :key="item" :label="item.positionStr+'-'+item.name" :label-width="addForm.labelWidth" style="color: orangered;">
      <el-input v-model="item.commentInfo" autocomplete="off"/>
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
          <el-select
                    v-model="tablePage.matchId"
                    placeholder="查询所有"
                    clearable
                    filterable
                  >
                    <el-option v-for="item in matchList"
                      :key="item.currentMatch"
                      :value="item.currentMatch.id"
                      :label="item.currentMatch.name"
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
           <el-table-column prop="state" label="展示状况" >
             <template #default="scope">
               <el-button v-if="scope.row.state==1" type="success" size="small" @click="onUpdateProgressClick(scope.row,0)"
                  >正在展示</el-button
                >
                <el-button plain v-else type="warning" size="small" @click="onUpdateProgressClick(scope.row,1)"
                  >点击展示</el-button
                >
             </template>
             </el-table-column>
             <el-table-column prop="name" label="小场名称" />
             <el-table-column prop="match.name" label="比赛名称" />
             <el-table-column prop="match.state" label="展示状况" >
             <template #default="scope">
               <el-button v-if="scope.row.match.state==1" type="success" size="small"
                  >比赛中</el-button
                >
                <el-button plain v-else type="warning" size="small"
                  >未进行</el-button
                >
             </template>
             </el-table-column>
             <el-table-column prop="blueTeam.name" label="蓝方队伍" />
             <el-table-column prop="redTeam.name" label="红方队伍" />
             <el-table-column prop="match.startTimeStr" label="开始时间" />
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

const fileList=ref([]);
const handleSuccess=(response, file, fileList)=> {
 console.log('上传成功', response);

 // 在这里处理上传成功后的逻辑，例如更新文件列表等
}
const handleError=(err, file, fileList)=> {
 console.error('上传失败', err);
 // 在这里处理上传失败后的逻辑，例如显示错误信息等
}
const beforeUpload=(file)=> {
//   const isJPGOrPNG = file.type === 'image/jpeg' || file.type === 'image/png';
//   const isLt2M = file.size / 1024 / 1024 < 0.5; // 文件大小不超过500kb
//   if (!isJPGOrPNG) {
//     ElMessage({
//         showClose: true,
//         message: '上传头像图片只能是 JPG/PNG 格式!',
//         type: 'error',
//       });
//     return false; // 不通过验证，阻止上传操作
//   }
//   if (!isLt2M) {
//     ElMessage({
//         showClose: true,
//         message: '上传头像图片大小不能超过 500KB!',
//         type: 'error',
//       });
//     return false; // 不通过验证，阻止上传操作
//   }
 return true; // 通过验证，允许上传操作继续进行
}

//#endregion
const userCode=userCodeStore();
const router=useRouter()
const onSubmit=()=>{
  pageDataChange();
}

//#region 删除
const onDeleteClick=(row)=>{
  http.post("/MatchPlayerComment/Delete",{
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
  addFormData.match=row.match;
  addFormData.blueTeam=row.blueTeam;
  addFormData.bluePlayerList=row.bluePlayerList;
  addFormData.redTeam=row.redTeam;
  addFormData.redPlayerList=row.redPlayerList;
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
    http.post("/MatchPlayerComment/Add",addFormData).then((res)=>{
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
    http.post("/MatchPlayerComment/Update",addFormData).then((res)=>{
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
    labelWidth:"15rem",
    visible:false,
    addOrUpdate:1,
    aOrUText:"添加"
});
//#endregion

//#region 表格
const tableData = reactive([]);
const tablePage=reactive({
    pageSize:20,
    pageCount:20,
    pageIndex:1,
    matchId:""
});
const pageUpdate=(pageIndex: number)=>{
    console.log(pageIndex);
    pageDataChange();
}
 const onUpdateProgressClick=(row,state)=>{
   http.post("/MatchPlayerComment/UpdateProgressState",{
     id:row.id,
     matchId:row.match.id,
     state:state
   })
   .then((res)=>{
       console.log(res);
       pageDataChange();
     });
}
const pageDataChange=()=>{
  http.post("/MatchPlayerComment/GetPageData",tablePage)
  .then((res)=>{
      console.log(res.data.data.items);
      tableData.splice(0, tableData.length, ...res.data.data.items);
      tablePage.pageCount=res.data.data.totalCount;
      console.log(res);
   });
}
//#endregion

//#region 初始化
const onChangeMatch=()=>{
  const selectMatch= matchList.find(f=>f.currentMatch.id==addFormData.match.id);

  addFormData.match.name=selectMatch.currentMatch.name;
  addFormData.match.startTime=selectMatch.currentMatch.startTime;
  addFormData.match.state=selectMatch.currentMatch.state;

  addFormData.blueTeam.id=selectMatch.currentMatch.blueTeam[0].id;
  addFormData.blueTeam.name=selectMatch.currentMatch.blueTeam[0].name;
  addFormData.blueTeam.avatar=selectMatch.currentMatch.blueTeam[0].avatar;
  addFormData.bluePlayerList.splice(0, addFormData.bluePlayerList.length, ...selectMatch.bluePlayerList);
  addFormData.redTeam.id=selectMatch.currentMatch.redTeam[0].id;
  addFormData.redTeam.name=selectMatch.currentMatch.redTeam[0].name;
  addFormData.redTeam.avatar=selectMatch.currentMatch.redTeam[0].avatar;
  addFormData.redPlayerList.splice(0, addFormData.redPlayerList.length, ...selectMatch.redPlayerList);
};
const addFormData=reactive({
  id:"",
  name:"",
  match:{
    id:"",
    name:"",
    startTime:"",
    state:0
  },
  blueTeam:{
    id:"",
    name:"",
    avatar:""
  },
  bluePlayerList:[
    {
      id:"",
      name:"",
      position:1,
      positionStr:"",
      rankName:1,
      school:"",
      commentInfo:""
    }
   ],
   redTeam:{
    id:"",
    name:"",
    avatar:""
  },
   redPlayerList:[
    {
      id:"",
      name:"",
      position:1,
      positionStr:"",
      rankName:1,
      school:"",
      commentInfo:""
    }
   ]
});
const matchList=reactive([
  {
    currentMatch:{
      id:"",
      name:"",
      startTime:"",
      startTimeStr:"",
      blueTeam:[
        {id:"",name:"",avatar:"",identity:0,identityStr:""}
      ],
      blueScore:0,
      redTeam:[
        {id:"",name:"",avatar:"",identity:0,identityStr:""}
      ],
      redScore:0,
      state:0
   },
   bluePlayerList:[
    {
      id:"",
      name:"",
      position:1,
      positionStr:"",
      rankName:1,
      school:"",
      commentInfo:""
    }
   ],
   redPlayerList:[
    {
      id:"",
      name:"",
      position:1,
      positionStr:"",
      rankName:1,
      school:"",
      commentInfo:""
    }
   ]
  }
]);
onMounted(()=>{
  if(userCode.codeVerify==false){
    router.push("login");
  }
   pageDataChange();
   http.get("/Match/GetAll")
   .then((res)=>{
       console.log(res.data.data);
       matchList.splice(0, matchList.length, ...res.data.data);
    });
});
//#endregion
</script>

<style scoped>

</style>
