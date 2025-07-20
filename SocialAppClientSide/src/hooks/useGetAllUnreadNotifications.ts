import { useQuery } from "@tanstack/react-query";
import { notificationModel } from "../models/models";
import axiosInstance from "../services/apiClient";

function getUnReadNotifications(userId:string)
{
    return axiosInstance
    .get<notificationModel[]>(`/Notifications/unread-notifications?UserId=${userId}`).then(res=>res.data);
}

export function useGetUnreadNotifications(userId:string)
{
    return useQuery({
        queryKey:["unread-notifications",userId],
        queryFn:()=>getUnReadNotifications(userId),
        enabled:!!userId
    });
}