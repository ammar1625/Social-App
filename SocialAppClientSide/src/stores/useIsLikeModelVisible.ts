
import { create } from "zustand";

interface isLikeModelVisibleStore
{
    isLikeVisible:boolean;
    setIsLikeVisible:(isVisible:boolean)=>void;
}

export const useIsLikeVisibleStore = create<isLikeModelVisibleStore>(set=>({
    isLikeVisible:false,
    setIsLikeVisible:(data)=>set(()=>({isLikeVisible:data}))
}));