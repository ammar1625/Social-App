import { useMutation } from "@tanstack/react-query";
import authClient from "../services/authClient";

function forgotPassWord(email:string)
{
    return authClient.post<boolean>(`/forgot-password?Email=${email}`).then(res=>res.data);
}

export function useForgotPassWord()
{
    return useMutation({
        mutationFn:forgotPassWord
    });
}