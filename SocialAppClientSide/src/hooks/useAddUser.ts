import { useMutation } from "@tanstack/react-query";
import { userToAdd, userToFetch } from "../models/models";
import authClient from "../services/authClient";

function AddUser(user:userToAdd)
{
    return authClient.post("/Register",user).then(res=>res.data);
}

export function useAddUser()
{
    return useMutation<userToFetch,Error,userToAdd>({
        mutationFn:AddUser
    });
}