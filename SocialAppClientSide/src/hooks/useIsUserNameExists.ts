import { useQuery } from "@tanstack/react-query";
import authClient from "../services/authClient";

function isUserNameExists(userName:string , userId:string|null)
{
    return userId? authClient.get(`/isExists-with-username?UserName=${userName}&UserId=${userId}`).then(res=>res.data):
    authClient.get(`/isExists-with-username?UserName=${userName}`).then(res=>res.data);
}

export function useIsUserNameExists(userName:string,userId:string)
{
    return useQuery({
        queryKey:["users",userName],
        queryFn:()=>isUserNameExists(userName,userId??null),
        enabled:!!userName 
    });
}