import { QueryClient, useMutation } from "@tanstack/react-query";
import axiosInstance from "../services/apiClient";

function changeNotificationStatus(notificationId:string|undefined)
{
    return axiosInstance.put(`/Notifications/change-status?NotificationId=${notificationId}`).then<boolean>(res=>res.data);
}

export function useChangeNotificationStatus(queryClient:QueryClient, userId:string)
{
    return useMutation({
        mutationFn:changeNotificationStatus,
        onSuccess:()=>{
            queryClient.invalidateQueries({queryKey:["unread-notifications",userId]});
        }
    });
}