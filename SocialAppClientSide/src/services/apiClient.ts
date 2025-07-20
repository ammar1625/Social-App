import axios from "axios";
import { userCurrentUserStore as useCurrentUserStore } from "../stores/useCurrentUserStore";
import { refreshTokenModel, refreshTokenResponse } from "../models/models";
import { useTokensStore } from "../stores/useTokensStore";
import { useNavigationStore } from "../stores/useNavigationStore";


/* export default axios.create({
    baseURL:"http://localhost:5250/api/Posts"
}); */

// Store navigation function reference
/* let navigateRef: (path: string) => void;

export const setNavigate = (navigate: (path: string) => void) => {
  navigateRef = navigate;
}; 

const redirectToLogin = () => {
  
  if (navigateRef) {
    navigateRef('/');
  } else {
    window.location.href = '/';
  }
};  */


const redirectToLogin = () => {
  const { navigate } = useNavigationStore.getState();
  if (navigate) {
    navigate('/');
  } else {
    window.location.href = '/';
  }
};






let isRefreshing = false;
let failedRequests: any[] = [];
/* const {user} = useCurrentUserStore.getState();
const {tokens} = useTokensStore.getState(); */



/* const refreshParams:refreshTokenModel = {
  userId :user.userId,
  refreshToken : user.refreshToken
} */

const axiosInstance =  axios.create({
  baseURL:"http://localhost:5250/api",
 /*  headers:{
      Authorization: `Bearer ${localStorage.getItem('accessToken')}`
  } */
});

// Request interceptor to inject token
axiosInstance.interceptors.request.use(config => {
    const { accessToken } = useTokensStore.getState().tokens;
    if (accessToken) {
      config.headers.Authorization = `Bearer ${accessToken}`;
    }
    return config;
  });
  
  //request interceptor to handle token refresh
  axiosInstance.interceptors.response.use(
    response => response,
    async error => {
      const originalRequest = error.config;
      if (error.response?.status === 401 && !originalRequest._retry) {
        if (isRefreshing) {
          return new Promise((resolve, reject) => {
            failedRequests.push({ resolve, reject });
          }).then(() => axiosInstance(originalRequest));
        }
  
        originalRequest._retry = true;
        isRefreshing = true;
  
        const { user } = useCurrentUserStore.getState();
        const { refreshToken:rToken } = useTokensStore.getState().tokens;
  
        if (!rToken) {
          redirectToLogin();
          return Promise.reject(error);
        }
  
        const refreshParams: refreshTokenModel = {
          userId: user.userId,
          refreshToken:rToken
        };
  
        try {
          const response = await axiosInstance.post<refreshTokenResponse>(
            "/Authentication/refresh-token",
            refreshParams
          );
  
          const { jwt, refreshToken: newRefreshToken } = response.data;
  
          useCurrentUserStore.getState().setCurrentUser({
            token: jwt,
            refreshToken: newRefreshToken
          });
  
          useTokensStore.getState().setTokens({
            accessToken: jwt,
            refreshToken: newRefreshToken
          });
  
          axiosInstance.defaults.headers.common["Authorization"] = `Bearer ${jwt}`;
          failedRequests.forEach(req => req.resolve());
          failedRequests = [];
  
          return axiosInstance(originalRequest);
        } catch (err) {
          redirectToLogin();
          return Promise.reject(err);
        } finally {
          isRefreshing = false;
        }
      }
  
      return Promise.reject(error);
    }
  );
  





export default axiosInstance;