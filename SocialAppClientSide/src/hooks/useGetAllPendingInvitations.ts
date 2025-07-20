
import axiosInstance from "../services/apiClient";
import { invitationModel } from "../models/models";
import { useQuery } from "@tanstack/react-query";


function getAllPindingInvitations(userId:string)
{
    return axiosInstance.get<invitationModel[]>(`/Invitations/pending-invitations/${userId}`).then(res=>res.data);
}

export function useGetAllPendingInvitations(userId:string)
{
    return useQuery({
        queryKey:["pending-invitations",userId],  
        queryFn:()=>getAllPindingInvitations(userId),
        enabled:!!userId,
        
    });
}