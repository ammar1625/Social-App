import { create } from "zustand";

interface logOutDialogVisibleStore
{
    isLogoutDialogVisible:boolean;
    setIsLogOutDialogVisible:(isVisible:boolean)=>void;
}

export const useIsLogoutDialogVisibleStore = create<logOutDialogVisibleStore>((set)=>({
    isLogoutDialogVisible:false,
    setIsLogOutDialogVisible:(data)=>set(()=>({isLogoutDialogVisible:data}))
}));