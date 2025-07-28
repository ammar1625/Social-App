import { useQuery } from "@tanstack/react-query";
import authClient from "../services/authClient";

function isEmailExists(email:string , userId:string|null)
{
    return userId? authClient.get<boolean>(`/isExists-with-email?Email=${email}&UserId=${userId}`).then(res=>res.data):
    authClient.get<boolean>(`/isExists-with-email?Email=${email}`).then(res=>res.data);
}

export function useIsEmailExists(email:string , userId?:string)
{
    return useQuery({
        queryKey:["users",email],
        queryFn:()=>isEmailExists(email,userId??null),
        enabled:!!email
    });
}