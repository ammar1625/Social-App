import { create } from "zustand";
import { invitationModel } from "../models/models";

interface invitationsStore
{
    invitations:invitationModel[];
    setInvitations:(data:invitationModel[])=>void;
}

export const useInvitationsStore = create<invitationsStore>(set=>({
    invitations:[],
    setInvitations:(data)=>set(()=>({invitations:data}))
}));