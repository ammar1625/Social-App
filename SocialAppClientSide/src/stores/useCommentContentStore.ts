import { create } from "zustand";

interface commentContentStore
{
    commentContent:string|undefined;
    setCommentContent:(content:string)=>void;
}

export const useCommentContentStore = create<commentContentStore>(set=>({
    commentContent:"",
    setCommentContent:(data)=>set(()=>({commentContent:data}))
}));