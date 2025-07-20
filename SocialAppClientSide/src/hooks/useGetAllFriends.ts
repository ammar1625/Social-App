import { useQuery } from "@tanstack/react-query";
import axiosInstance from "../services/apiClient";
import { friendshipMemberToFetchModel } from "../models/models";

function getAllFriends(userId:string)
{
    return axiosInstance.get<friendshipMemberToFetchModel[]>(`/FriendshipMembers/friendslist?UserId=${userId}`)
    .then(res=>res.data);
}

export function useGetAllFriends(userId:string)
{
    return useQuery({
        queryKey:["friends-list",userId],
        queryFn:()=>getAllFriends(userId),
        enabled:!!userId
    });
}