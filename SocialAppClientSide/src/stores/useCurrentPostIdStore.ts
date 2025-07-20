import { create } from "zustand";

interface currentPostIdStore
{
    postId:string;
    setCurrentPostId:(postId:string)=>void;
}

export const useCurrentPostIdStore = create<currentPostIdStore>(set=>({
    postId:"",
    setCurrentPostId:(data)=>set(()=>({postId:data}))
}));