import LS from './localStorage'
export const LoginUserUtils={
    //登录
    Login(value:LoginTeacher){
        //保存token
        LS.set("accessToken",value.accessToken);
        //保存教师信息
        LS.set("teacher",value);
    },
    //检查是否登录
    IsLogin(){
       const at=LS.get("accessToken");
       return at?true:false;
    },
    //退出登录
    LoginOut(){
         //保存token
         LS.remove("accessToken");
         //保存教师信息
         LS.remove("teacher");
    },
    GetLoginTeacher():LoginTeacher{
      const teacher=LS.get("teacher");
        return teacher;
    }
}
export interface LoginTeacher{
    userName?:string,
    teacherId:number,
    rolePermission:number,
    accessToken?:string,
    isSuccess:boolean
}
