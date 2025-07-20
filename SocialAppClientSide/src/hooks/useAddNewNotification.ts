import { useMutation } from "@tanstack/react-query";
import { notificationModel, notificationToAddModel } from "../models/models";
import axiosInstance from "../services/apiClient";

function addNotification(model:notificationToAddModel)
{
    return axiosInstance.post<notificationModel>(`/Notifications/add-notification`,model).then(res=>res.data);
}

export function useAddNotification()
{
    return useMutation({
        mutationFn:addNotification
    });
}