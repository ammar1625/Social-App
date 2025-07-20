import { useMutation } from "@tanstack/react-query";
import { loginModel, loginResponseModel } from "../models/models";
import authClient from "../services/authClient";

function login(loginCredentials:loginModel)
{
    return authClient.post<loginResponseModel>(`/Login`,loginCredentials).then(res=>res.data);
}

export function useLogIn()
{
    return useMutation<loginResponseModel,Error,loginModel>({
        mutationFn:login,
    });
}
