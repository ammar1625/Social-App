import { create } from "zustand";

interface disconnectingMessageVisible
{
    isDisconnectingMessageVisible:boolean;
    setIsDisconnectingMessageVisible:(isVisible:boolean)=>void;
}

export const useIsDisconnectingMessageVisible = create<disconnectingMessageVisible>(set=>({
    isDisconnectingMessageVisible:false,
    setIsDisconnectingMessageVisible:(data)=>set(()=>({isDisconnectingMessageVisible:data}))
}));