import { useMutation } from "@tanstack/react-query";
import { changePassWordResponseModel, resetPassWordModel } from "../models/models";
import axiosInstance from "../services/apiClient";

function changePassWord(model:resetPassWordModel)
{
    return axiosInstance.put<changePassWordResponseModel>(`/Users/change-password`,model).then(res=>res.data);
}

export function useChangePassWord()
{
    return useMutation({
        mutationFn:changePassWord
    });
}