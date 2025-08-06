import { useMutation } from "@tanstack/react-query";
import { validatePassWordModel, validatePassWordResponseModel } from "../models/models";
import axiosInstance from "../services/apiClient";

function validatePassWord(model:validatePassWordModel)
{
    return axiosInstance.post<validatePassWordResponseModel>(`/Users/verify-password`,model).then(res=>res.data);
}

export function useValidatePassWord()
{
    return useMutation({
        mutationFn:validatePassWord
    });
}