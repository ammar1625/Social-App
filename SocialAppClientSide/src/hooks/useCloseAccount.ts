import { useMutation } from "@tanstack/react-query";
import { closeAccountModel, closeAccountResponseModel } from "../models/models";
import axiosInstance from "../services/apiClient";

function closeAccount(model:closeAccountModel)
{
    return axiosInstance.delete<closeAccountResponseModel>(`/Users/delete-User`,{
        data:model
    }).then(res=>res.data);
}

export function useCloseAccount()
{
    return useMutation({
        mutationFn:closeAccount
    });
}