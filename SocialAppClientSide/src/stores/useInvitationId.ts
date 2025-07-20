import { create } from "zustand";

interface invitationIdStore
{
    invitationId:string;
    setInvitationId:(data:string)=>void;
}

export const useInvitationId = create<invitationIdStore>(set=>({
    invitationId:"",
    setInvitationId:(data)=>set(()=>({invitationId:data}))
}));