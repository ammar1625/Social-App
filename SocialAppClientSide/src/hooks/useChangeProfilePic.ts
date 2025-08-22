import { useMutation } from "@tanstack/react-query";
import { changeProfilePicModel } from "../models/models";
import axiosInstance from "../services/apiClient";

function changeProfilePic(model:changeProfilePicModel)
{
    const form = new FormData();
    form.append("UserId",model.userId);
    if(model.profilePic)
    {
        form.append("ProfilePic",model.profilePic);
    }
    return axiosInstance.put<string>(`/Users/update-profile-pic`,form).then(res=>res.data);
}

export function useChangeProfilePic()
{
    return useMutation({
        mutationFn:changeProfilePic
    });
}