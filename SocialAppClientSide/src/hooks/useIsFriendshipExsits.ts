import { useQuery } from "@tanstack/react-query";
import axiosInstance from "../services/apiClient";

function isFriendshipExists(currentUserId:string,targetUserId:string)
{
    return axiosInstance
    .get<boolean>(`/Friendships/isFriendshipExists?CurrentUserId=${currentUserId}&TargetUserId=${targetUserId}`)
    .then(res=>res.data);
}

export function useIsFriendshipExists(currentUserId:string,targetUserId:string)
{
    return useQuery({
        queryKey:["isFriendship-exists",currentUserId,targetUserId],
        queryFn:()=>isFriendshipExists(currentUserId,targetUserId),
        enabled:(!!currentUserId&&!!targetUserId)
    });
}