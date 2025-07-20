import { create } from "zustand";
import { tokensModel } from "../models/models";

interface tokensStore
{
    tokens:tokensModel;
    setTokens:(tokens:Partial<tokensModel>)=>void;
}

export const useTokensStore = create<tokensStore>((set)=>({
    tokens:
    {
        accessToken:"",
        refreshToken:""
    } 
    ,
    setTokens:(data)=>set((store)=>({tokens:{...store.tokens,...data}}))
}))