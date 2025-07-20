import { useQuery } from "@tanstack/react-query";
import authClient from "../services/authClient";

function isUserNameExists(userName:string)
{
    return authClient.get(`/isExists-with-username/${userName}`).then(res=>res.data)
}

export function useIsUserNameExists(userName:string)
{
    return useQuery({
        queryKey:["users",userName],
        queryFn:()=>isUserNameExists(userName),
        enabled:!!userName
    });
}