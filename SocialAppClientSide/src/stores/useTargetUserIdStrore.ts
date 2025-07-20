import { create } from "zustand";

interface targetUserIdStore
{
    userId:string;
    setUserId:(userId:string)=>void;
}

export const useTargetUserIdStore = create<targetUserIdStore>(set=>({
userId:"",
setUserId:(Id)=>set(()=>({userId:Id}))
}));