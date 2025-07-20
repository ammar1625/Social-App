import { useQuery } from "@tanstack/react-query";
import axiosInstance from "../services/apiClient";
import { invitationModel } from "../models/models";

function isInvitationExists(senderId:string,recieverId:string)
{
    return axiosInstance
    .get<invitationModel>(`/Invitations/isInvitationExists?SenderId=${senderId}&RecieverId=${recieverId}`).then(res=>res.data);
}

export function useIsInvitationExists(senderId:string,recieverId:string)
{
    return useQuery({
        queryKey:["isInvitationExists",senderId,recieverId],
        queryFn:()=>isInvitationExists(senderId,recieverId),
        enabled:(!!senderId&&!!recieverId)
    });
}