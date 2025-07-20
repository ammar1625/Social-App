import { useMutation } from "@tanstack/react-query";
import { conversationModel } from "../models/models";
import axiosInstance from "../services/apiClient";

function addNewConversation()
{
    return axiosInstance.post<conversationModel>(`/Conversations/add-new`).then(res=>res.data);
}

export function useAddNewConversation()
{
    return useMutation({
        mutationFn:addNewConversation
    });
}