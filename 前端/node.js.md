## NodeJS
* * * 
## 包管理
~~很好~~
<u>下划线</u>
*斜体*  

**粗体**  

***粗斜体***  
### 包管理 
原文链接： https://blog.csdn.net/weixin_65793170/article/details/136384723
原文链接：https://blog.csdn.net/qq_37550440/article/details/124266366
npm官网：  https://www.npmjs.com/
* #### npm
> npm全称Node Package Manager，是 Node.js 平台的默认软件包管理器，用于安装、发布和管理 Node.js 应用程序和软件包。它是一个命令行工具，允许开发人员在他们的项目中轻松地管理依赖项、安装软件包、更新软件包版本以及执行其他与软件包相关的任务。npm 是 nodejs 中的一部分，通常与 nodejs 一起安装和更新。当你安装了 nodejs，npm 也随之安装。
* #### cnpm
> cnpm（China Node Package Manager）是一个为中国大陆用户定制的 npm（Node Package Manager）镜像，由阿里巴巴的淘宝团队开发和维护。由于 npm 的原始服务器位于国外，对于中国大陆的用户来说，下载和安装 Node.js 包时可能会遇到速度较慢或连接不稳定的问题。为了解决这个问题，cnpm 镜像被创建出来，以提高下载速度和稳定性。
* #### pnpm 
> pnpm是一个高效的 npm 包管理工具，它旨在解决包依赖管理时的一些常见问题。与npm和yarn不同，pnpm使用一种称为“符号链接”的方法来管理包依赖，称为硬链接（hard links），这可以节省磁盘空间并提高安装速度，并确保同一个包的不同版本之间共享尽可能多的代码
#### 一.npm
##### 1.初始化项目
> **npm init**
> 初始化项目生成package.json文件,该文件包含了项目的元数据和依赖信息
> **npm init -y**
> 默认初始化，省去敲回车
##### 2.安装依赖包
###### 执行过程 npm install->config->[npm config list]-->
###### -->项目级.npmrc->用户级.npmrc->全局的.npmrc->npm内置.npmrc
``` javascript
//安装所有node_moudles依赖包
npm install  
//安装axios依赖包
npm install axios 
//安装指定版本
npm install axios@1.8.2 
//默认值就是S** 配置在package.json==》dependencies下，当前目录node_modules,程序运行必要依赖
npm install axios@1.8.2 -S   
//配置在package.json==》devDependencies下，当前目录node_modules,开发测试时依赖
npm install axios@1.8.2 -D 
//全局安装文件,作用于全局环境下,模块安装到操作系统上 一般会安装到AppDataAppData\Roaming\npm目录下
npm install axios@1.8.2 -g  
//卸载axios包
npm uninstall axios 

//更新所有包/只更新axios包到最新版本
npm update
npm update axios

```
###### 依赖包版本相关
``` javascript
//列出所有安装包
//depth用于指定展示依赖关系的深度,0表示只显示直接安装的模块
npm list --depth=0
//查看本地安装版本
npm ls axio
//查看全局安装版本
npm ls axios -g
//查看npm环境配置
npm config list
//npm查看历史版本
npm view axios versions

```
##### 3.其他命令
> **npm run dev**
> 查看package.json文件中Scripts项  {"dev":"vite"}
> vite对应node_modules下的.bin文件夹下，里面包含可执行文件
###### 仓库相关
> **npm cache clean --force**
> 清除缓存
> **npm get registry**
> **npm config get registry**
> 查看当前源地址
> **npm set registry https://repo.huaweicloud.com/repository/npm/**
> 设置源地址 
###### 修改npm全局模块路径和cache路径
>在对应位置建立好文件夹
>npm config set prefix "D:\NodeJS\node_global"
>npm config set cache "D:\NodeJS\node_cache"
>配置环境变量
>增加系统变量NODE_PATH=D:\NodeJS\node_global\node_modules
>系统变量Path增加一列D:\NodeJS\node_global
###### 常用的镜像源地址
> 官方源（npm registry）：https://registry.npmjs.org/
> 淘宝NPM镜像源：https://registry.npmmirror.com/
> cnpm镜像源：http://r.cnpmjs.org/
> 阿里云NPM镜像源：https://npm.aliyun.com/
> 腾讯云NPM镜像源：https://mirrors.cloud.tencent.com/npm/
> 华为云NPM镜像源：https://mirrors.huaweicloud.com/repository/npm/
> 网易NPM镜像源：https://mirrors.163.com/npm/
> 中国科学技术大学开源镜像站：http://mirrors.ustc.edu.cn/npm/
> 清华大学开源镜像站：https://mirrors.tuna.tsinghua.edu.cn/npm/

