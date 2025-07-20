import { useQuery } from "@tanstack/react-query";
import { likeToFetchModel } from "../models/models";
import axiosInstance from "../services/apiClient";

function getLikes(postId:string)
{
    return axiosInstance.get<likeToFetchModel[]>(`/Like/Likes-list/${postId}`).then(res=>res.data);
}

export function useGetAllLikes(postId:string)
{   
    return useQuery({
        queryKey:["likes",postId],
        queryFn:()=>getLikes(postId),
        enabled:!!postId
    });
}