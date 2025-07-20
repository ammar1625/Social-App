import { QueryClient, useMutation } from "@tanstack/react-query";
import { invitationModel, invitationToAddModel } from "../models/models";
import axiosInstance from "../services/apiClient";

function addNewInvitation(model:invitationToAddModel)
{
    return axiosInstance.post<invitationModel>(`/Invitations/add-new`,model).then(res=>res.data);
}

export function useAddNewInvitation(queryClient:QueryClient,senderId:string,recieverId:string)
{
    return useMutation({
        mutationFn:addNewInvitation,
        onSuccess:()=>{
            queryClient.invalidateQueries({queryKey:["isInvitationExists",senderId,recieverId]});
        }
    });
}