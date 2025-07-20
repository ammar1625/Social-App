
import { create } from "zustand";
import { loginModel } from "../models/models";

interface loginCredentialsStore
{
    credentials:loginModel;
    setCredentials:(loginCredentials:Partial<loginModel>)=>void;
}

export const useLoginCredentialsStore = create<loginCredentialsStore>((set)=>({
    credentials:{
        email:"",
        passWord:""
    },
    setCredentials:(credential)=>set((store)=>({credentials:{...store.credentials,...credential}}))
}));