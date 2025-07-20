import { QueryClient, useMutation } from "@tanstack/react-query";
import { changeinvitationsStatusMOdel, invitationModel } from "../models/models";
import axiosInstance from "../services/apiClient";




function changeInvitationStatus(model:changeinvitationsStatusMOdel)
{
    return axiosInstance.put(`/Invitations/change-status`,model).then(res=>res.data);
}

export function useChangeInvitationStatus(queryClient:QueryClient , userId:string ,currentUserId:string,targetUserId:string, senderId:string, recieverId:string)
{
    return useMutation<invitationModel,Error,changeinvitationsStatusMOdel>({
        mutationFn:changeInvitationStatus , 

        onSuccess:()=>{
            
            queryClient.invalidateQueries({queryKey:["pending-invitations",userId]});
            queryClient.invalidateQueries({queryKey:["friends-list"/* ,userId */]});
            queryClient.invalidateQueries({queryKey:["isFriendship-exists",currentUserId,targetUserId]});
            queryClient.invalidateQueries({queryKey:["isInvitationExists",senderId,recieverId]});
           setTimeout(() => {
            queryClient.invalidateQueries({queryKey:["posts",userId]});
           }, 7000); 

           /* const queryCache = queryClient.getQueryCache();
            const allQueries = queryCache.getAll();

            console.group("All Queries in Cache");
            allQueries.forEach((query) => {
            console.log("Query Key:", query.queryKey);
            console.log("State:", query.state);
            console.log("----------------------------");
            });
            console.groupEnd(); */

        }
    });
}