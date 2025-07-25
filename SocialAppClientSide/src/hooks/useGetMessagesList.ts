import { useQuery } from "@tanstack/react-query";
import { messageModel } from "../models/models";
import axiosInstance from "../services/apiClient";

function getMessagesList(conversationId:string)
{
    return axiosInstance.get<messageModel[]>(`/Messages/messages/${conversationId}`).then(res=>res.data);
}

export function useGetMessagesList(conversationId:string)
{
    return useQuery({
        queryKey:['messages',conversationId],
        queryFn:()=>getMessagesList(conversationId),
        enabled:!!conversationId
    });
}