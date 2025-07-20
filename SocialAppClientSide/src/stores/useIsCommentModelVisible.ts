
import { create } from "zustand";

interface isCommentModelVisibleStore
{
    isCommentVisible:boolean;
    setIsCommentVisible:(isVisible:boolean)=>void;
}

export const useIsCommentVisibleStore = create<isCommentModelVisibleStore>(set=>({
    isCommentVisible:false,
    setIsCommentVisible:(data)=>set(()=>({isCommentVisible:data}))
}));