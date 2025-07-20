import { useQuery } from "@tanstack/react-query";
import axiosInstance from "../services/apiClient";
import { userToFetch } from "../models/models";

function getUsersList(nameFilter:string)
{
    return axiosInstance.get<userToFetch[]>(`/Users/users-list?NameFilter=${nameFilter}`).then(res=>res.data);
}

export function useGetFilteredUsersList(nameFilter:string)
{
    return useQuery({
        queryKey:["users-list",nameFilter],
        queryFn:()=>getUsersList(nameFilter),
        enabled:!!nameFilter
    });
}