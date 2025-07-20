import { useQuery } from "@tanstack/react-query";
import { userToFetch } from "../models/models";
import axiosInstance from "../services/apiClient";

function getUserById(userId:string)
{
    return axiosInstance.get<userToFetch>(`/Users/GetById/${userId}`).then(res=>res.data);
}

export function useGetUserById(userId:string)
{
    return useQuery({
        queryKey:["user-id",userId],
        queryFn:()=>getUserById(userId),
        enabled:!!userId
    });
}