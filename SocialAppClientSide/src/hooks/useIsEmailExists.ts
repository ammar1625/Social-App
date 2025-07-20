import { useQuery } from "@tanstack/react-query";
import authClient from "../services/authClient";

function isEmailExists(email:string)
{
    return authClient.get<boolean>(`/isExists-with-email/${email}`).then(res=>res.data);
}

export function useIsEmailExists(email:string)
{
    return useQuery({
        queryKey:["users",email],
        queryFn:()=>isEmailExists(email),
        enabled:!!email
    });
}