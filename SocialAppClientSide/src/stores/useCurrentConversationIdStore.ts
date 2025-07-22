import { create } from "zustand";

interface conversationIdStore
{
    conversationId:string;
    setConversationId:(data:string)=>void;
}

export const useCurrentConversationIdStore = create<conversationIdStore>(set=>({
    conversationId:"",
    setConversationId:(data:string)=>set(()=>({conversationId:data})),
}));