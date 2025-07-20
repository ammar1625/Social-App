import { QueryClient, useMutation } from "@tanstack/react-query";
import { invitationToAddModel } from "../models/models";
import axiosInstance from "../services/apiClient";

function cancelInvitation(model:invitationToAddModel)
{
    return axiosInstance.delete<boolean>(`/Invitations/delete`,{data:model}).then(res=>res.data);
}

export function useCancelInvitation(queryClient:QueryClient,senderId:string,recieverId:string)
{
    return useMutation({
        mutationFn:cancelInvitation,
        onSuccess:()=>{
            queryClient.invalidateQueries({queryKey:["isInvitationExists",senderId,recieverId]});
            queryClient.invalidateQueries({queryKey:["pending-invitations",senderId]});
        }
    });
}