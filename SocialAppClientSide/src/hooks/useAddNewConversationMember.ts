import { useMutation } from "@tanstack/react-query";
import { conversationMemberToAddModel, conversationMemberToFetchModel } from "../models/models";
import axiosInstance from "../services/apiClient";
//import { NavigateFunction } from "react-router-dom";

function addNewConversationMember(model:conversationMemberToAddModel)
{
    return axiosInstance.post<conversationMemberToFetchModel>(`/ConverastionMembers/add-new`,model).then(res=>res);
}

export function useAddNewConversationMember(/* navigate:NavigateFunction , mutationCount:number */)
{
    return useMutation({
        mutationFn:addNewConversationMember
        /* onSuccess:()=>{
            if(mutationCount===2)
            {
                navigate("/conversation");
            }
        } */
    });
}