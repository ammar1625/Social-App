import { useMutation } from "@tanstack/react-query";
import { updateUserModel, user} from "../models/models";
import axiosInstance from "../services/apiClient";

function updateCredentials(model:updateUserModel)
{
    return axiosInstance.put<user>(`/Users/update-credentials`,model).then(res=>res.data);
}

export function useUpdateCredentials()
{
    return useMutation({
        mutationFn:updateCredentials,
        
    });
}