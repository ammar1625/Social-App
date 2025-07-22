import { useQuery } from "@tanstack/react-query";
import axiosInstance from "../services/apiClient";
import { conversationMember } from "../models/models";

function getConversationMembersList(userId:string)
{
    return axiosInstance.get<conversationMember[]>(`/ConverastionMembers/conversations-list/${userId}`).then(res=>res.data);
}

export function useGetConversationByMembersList(userId:string)
{
    return useQuery({
        queryKey:["conversation-members-list",userId],
        queryFn:()=>getConversationMembersList(userId),
        enabled:!!userId
    });
}