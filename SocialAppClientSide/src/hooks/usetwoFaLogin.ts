import { useQuery } from "@tanstack/react-query";
import authClient from "../services/authClient";
import { userToFetch } from "../models/models";

function twoFaLogin(otpCode:number)
{
    return authClient.post<userToFetch>(`/2falogin`,otpCode, {
        headers: {
          'Content-Type': 'application/json',
        }
        }).then(res=>res.data);
}

export function useTwoFaLogin(otpCode:number)
{
    return useQuery({
        queryKey:["twofalogin",otpCode],
        queryFn:()=>twoFaLogin(otpCode),
        enabled:!!otpCode
    });
}