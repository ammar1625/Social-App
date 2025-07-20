import { useQuery } from "@tanstack/react-query";
import { conversationModel, conversationToFetchModel } from "../models/models";
import axiosInstance from "../services/apiClient";

function getConversation(model:conversationToFetchModel)
{
    return axiosInstance.get<conversationModel>(`/Conversations/get-by-members?CurrentUserId=${model.currentUserId}&TargetUserId=${model.targetUserId}`).then(res=>res.data);
}

export function useGetConversationByMembers(model:conversationToFetchModel)
{
    return useQuery({
        queryKey:["conversation",model.currentUserId , model.targetUserId],
        queryFn:()=>getConversation(model),
        enabled:(!!model.targetUserId && !!model.currentUserId)
    });
}