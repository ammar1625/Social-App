import { useMutation } from "@tanstack/react-query";
import axiosInstance from "../services/apiClient";
import { friendshipModel } from "../models/models";

function addNewFriendship()
{
    return axiosInstance.post<friendshipModel>(`/Friendships/add-new`).then(res=>res.data);
}

export function useAddNewFriendship()
{
    return useMutation({
        mutationFn:addNewFriendship
    });
}