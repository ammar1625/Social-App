import { create } from "zustand";

interface isOverlayVisibleStore
{
    isOverlayVisible:boolean;
    setIsOverlayVisible:(isVisible:boolean)=>void;
}

export const useIsOverlayVisibleStore = create<isOverlayVisibleStore>(set=>({
    isOverlayVisible:false,
    setIsOverlayVisible:(data)=>set(()=>({isOverlayVisible:data}))
}));