import { useQuery } from "@tanstack/react-query";
import axiosInstance from "../services/apiClient";
import { postModel, postsModel as currentUserPostsModel } from "../models/models";

function getCurrentUserPosts(model:currentUserPostsModel)
{
    return axiosInstance.get<postModel[]>(`/Posts/user-posts?UserId=${model.userId}&CurrentUserId=${model.currentUserId}`).then(res=>res.data);
}

export function useGetCurrentUserPosts(model:currentUserPostsModel)
{
    return useQuery({
        queryKey:["current-user-posts",model.userId],
        queryFn:()=>getCurrentUserPosts(model),
        enabled:!!model.userId && !!model.currentUserId
    });
}