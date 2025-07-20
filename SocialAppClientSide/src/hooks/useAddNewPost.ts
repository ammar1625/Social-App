import { QueryClient, useMutation } from "@tanstack/react-query";
import { postToAddModel } from "../models/models";
import axiosInstance from "../services/apiClient";

function addNewPost(model:postToAddModel)
{
    const formdata = new FormData();
    if(model.content)
    {
        formdata.append("Content",model.content);
    }
    if(model.media)
    {
        formdata.append("Media",model.media);
    }
    formdata.append("UserId",model.userId);
    return axiosInstance.post(`/Posts/addpost`,formdata , 
        {
            headers: {
              "Content-Type":"multipart/form-data"
            }
        }
    );
}

export function useAddNewPost(queryClient:QueryClient , userId:string)
{
    return useMutation({
        mutationFn:addNewPost,
        onSuccess:()=>{
            queryClient.invalidateQueries({queryKey:["posts",userId]});
        }
    });
}