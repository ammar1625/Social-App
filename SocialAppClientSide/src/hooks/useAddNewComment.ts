import { QueryClient, useMutation } from "@tanstack/react-query";
import { commentToAddModel, commentTofetchModel } from "../models/models";
import axiosInstance from "../services/apiClient";

function addNewComment(model:commentToAddModel)
{
    return axiosInstance.post<commentTofetchModel>(`/Comments/add-new`,model).then(res=>res.data);
}

export function useAddNewComment(queryClient:QueryClient , postId:string , userId:string)
{
    return useMutation({
        mutationFn:addNewComment,

        onSuccess:()=>{
            queryClient.invalidateQueries({queryKey:["comments",postId]});
            queryClient.invalidateQueries({queryKey:["current-user-posts",userId]});
            queryClient.invalidateQueries({queryKey:["posts",userId]});
        }
    });
}