import { useQuery } from "@tanstack/react-query";
import apiClient from "../services/apiClient";
import { postModel } from "../models/models";

function getPosts(userId:string)
{
    return apiClient.get<postModel[]>(`/Posts/user-friends-posts/${userId}`).then(res=>res.data);
}

export function useGetUserAndFriendsPosts(userId:string)
{
    return useQuery({
        queryKey:["posts",userId],
        queryFn:()=>getPosts(userId),
        enabled:!!userId,
    });
}