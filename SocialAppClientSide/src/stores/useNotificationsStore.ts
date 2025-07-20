import { create } from "zustand";
import { notificationModel } from "../models/models";

interface NotificationsStore
{
    notifications:notificationModel[];
    setNotifications:(data:notificationModel[])=>void;
}

export const useNotificationsStore = create<NotificationsStore>(set=>({
    notifications:[],
    setNotifications:(data)=>set(()=>({notifications:data}))
}));