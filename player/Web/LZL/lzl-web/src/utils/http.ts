import axios from 'axios';
import type { AxiosInstance, AxiosResponse }from 'axios';
import {LoginUserUtils} from './userUtils'
const proxy_path = import.meta.env.VITE_API_Proxy_PATH;
// 创建axios实例
const http: AxiosInstance = axios.create({
  //baseURL: '/api', // 你的基础URL
  baseURL: proxy_path,
  timeout: 10000, // 请求超时时间
});

// 添加请求拦截器
http.interceptors.request.use(
  (config) => {
    // 在发送请求之前做些什么，例如设置token等
    // config.headers['Authorization'] = `Bearer ${token}`;
    const token= LoginUserUtils.GetLoginTeacher()?.accessToken;

    config.headers['Authorization'] = `Bearer ${token}`;

    console.log("axios请求")
    console.log(config);
    return config;
  },
  // eslint-disable-next-line @typescript-eslint/no-explicit-any
  (error:any) => {
    // 对请求错误做些什么
    return Promise.reject(error);
  }
);

// 添加响应拦截器
http.interceptors.response.use(
  (response: AxiosResponse) => {
    // 对响应数据做点什么
    console.log("axios响应")
    console.log(response)
    return response;
  },
  (error:unknown) => {
    // 对响应错误做点什么
    return Promise.reject(error);
  }
);
export default http;
