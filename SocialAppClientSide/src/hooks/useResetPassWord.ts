import { useMutation } from "@tanstack/react-query";
import { changePassWordModel } from "../models/models"
import authClient from "../services/authClient"
function resetPassWord(resetPassWordModel:changePassWordModel)
{
    return authClient.put<boolean>(`/reset-password`,resetPassWordModel).then(res=>res.data);
}

export function useResetPassWord()
{
    return useMutation({
        mutationFn:resetPassWord
    });
}