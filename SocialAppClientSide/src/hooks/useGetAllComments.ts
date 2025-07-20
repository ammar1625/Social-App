import { useQuery } from "@tanstack/react-query";
import { commentTofetchModel } from "../models/models";
import axiosInstance from "../services/apiClient";

function getAllComments(postId:string)
{
    return axiosInstance.get<commentTofetchModel[]>(`/Comments/comments-list/${postId}`).then(res=>res.data);
}

export function useGetAllComments(postId:string)
{
    return useQuery({
        queryKey:["comments",postId],
        queryFn:()=>getAllComments(postId),
        enabled:!!postId
    });
}