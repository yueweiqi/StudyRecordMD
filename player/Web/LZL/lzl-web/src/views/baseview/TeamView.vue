<template>
 <el-dialog v-model="addForm.visible" :title="addForm.aOrUText">
    <el-form :model="addForm">
      <el-form-item label="队伍名称" :label-width="addForm.labelWidth">
        <el-input v-model="addFormData.name" autocomplete="off" />
      </el-form-item>
      <el-form-item v-show="false" label="队伍头像" :label-width="addForm.labelWidth">
        <el-input v-model="addFormData.avatar" autocomplete="off" />
      </el-form-item>
      <el-form-item label="队伍头像" :label-width="addForm.labelWidth">
        <div class="flex-column"><el-avatar shape="square" size="large" :src="fileBasePath+addFormData.avatar" />
        <el-upload
            class="upload-demo"
            :action="httpBasePath+ '/Team/AvatarPost'"
            :on-success="handleSuccess"
            :on-error="handleError"
            :file-list="fileList"
            :show-file-list="false"
            :before-upload="beforeUpload">
            <el-button size="small" type="primary">点击上传</el-button>
          </el-upload></div>

      </el-form-item>
      <el-form-item label="队伍宣言" :label-width="addForm.labelWidth">
        <el-input v-model="addFormData.manifesto" autocomplete="off" />
      </el-form-item>
      <el-form-item label="说明" :label-width="addForm.labelWidth">
        <el-input v-model="addFormData.description" autocomplete="off" />
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
    <el-form-item label="队伍名称">
      <el-input v-model="tablePage.name" />
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
            <el-table-column prop="name" label="队伍名称" />
            <el-table-column label="队伍头像" >
              <template #default="scope">
                <el-avatar shape="square" size="large" :src="fileBasePath+scope.row.avatar" />
              </template>
            </el-table-column>
            <el-table-column prop="manifesto" label="队伍宣言" />
            <el-table-column prop="description" label="说明" />
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

const fileBasePath = ref(import.meta.env.VITE_File_BASE_URL);
const httpBasePath = import.meta.env.VITE_API_BASE_URL+import.meta.env.VITE_API_Proxy_PATH;

//#region 文件上传
const fileList=ref([]);
const handleSuccess=(response, file, fileList)=> {
  console.log('上传成功', response);
  addFormData.avatar=response.data;
  // 在这里处理上传成功后的逻辑，例如更新文件列表等
}
const handleError=(err, file, fileList)=> {
  console.error('上传失败', err);
  // 在这里处理上传失败后的逻辑，例如显示错误信息等
}
const beforeUpload=(file)=> {
  const isJPGOrPNG = file.type === 'image/jpeg' || file.type === 'image/png';
  const isLt2M = file.size / 1024 / 1024 < 0.5; // 文件大小不超过500kb
  if (!isJPGOrPNG) {
    ElMessage({
        showClose: true,
        message: '上传头像图片只能是 JPG/PNG 格式!',
        type: 'error',
      });
    return false; // 不通过验证，阻止上传操作
  }
  if (!isLt2M) {
    ElMessage({
        showClose: true,
        message: '上传头像图片大小不能超过 500KB!',
        type: 'error',
      });
    return false; // 不通过验证，阻止上传操作
  }
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
  http.post("/Team/Delete",{
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
  addFormData.manifesto=row.manifesto;
  addFormData.description=row.description;
  addFormData.avatar=row.avatar;
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
    http.post("/Team/Add",addFormData).then((res)=>{
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
    http.post("/Team/Update",addFormData).then((res)=>{
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
  avatar:"",
  manifesto:"",
  description:"",
});
//#endregion

//#region 表格

interface Player {
  name: string;
  manifesto:string;
  avatar:string;
  description:string;
}
const tableData = reactive<Player[]>([]);
const tablePage=reactive({
    pageSize:20,
    pageCount:20,
    pageIndex:1,
    name:"",
});
const pageUpdate=(pageIndex: number)=>{
    console.log(pageIndex);
    pageDataChange();
}
const pageDataChange=()=>{
  http.post("/Team/GetPageData",tablePage)
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
   pageDataChange();
});
//#endregion
</script>

<style scoped>

</style>
