import { useMutation } from "@tanstack/react-query";
import { logOutResponseModel } from "../models/models";
import axiosInstance from "../services/apiClient";

function logOut(userId:string)
{
    return axiosInstance.post<logOutResponseModel>(`/Users/log-out`,JSON.stringify(userId),
{headers:{
    "Content-Type":"application/json"
}}
).then(res=>res.data);
}

export function useLogOut()
{
    return useMutation({
        mutationFn:logOut
    });
}