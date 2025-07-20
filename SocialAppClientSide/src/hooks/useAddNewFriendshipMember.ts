import { useMutation } from "@tanstack/react-query";
import { friendshipMemberToAddModel } from "../models/models";
import axiosInstance from "../services/apiClient";

function addNewFriendshipMember(model:friendshipMemberToAddModel)
{
    return axiosInstance.post(`/FriendshipMembers/add-new`,model).then(res=>res.data);
}

export function useAddNewFriendshipMember()
{
    return useMutation({
        mutationFn:addNewFriendshipMember,
    });
}