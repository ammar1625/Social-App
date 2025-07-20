import { useQuery } from "@tanstack/react-query";
import { likeToAddModel } from "../models/models";
import axiosInstance from "../services/apiClient";

function isLikeExists(model:likeToAddModel)
{
    return axiosInstance.get<boolean>(`like/isLiked?PostId=${model.postId}&UserId=${model.userId}`).then(res=>res.data);
}

export function useIsLikeExists({postId,userId}:likeToAddModel)
{
    return useQuery({
        queryKey:["likes","isLiked",postId,userId],
        queryFn:()=>isLikeExists({postId,userId}),
        enabled:(!!postId && !!userId)
    });
}