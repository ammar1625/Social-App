import { create } from "zustand";
import { userToFetch } from "../models/models";

interface currentUserStore
{
    user:userToFetch;
    setCurrentUser:(currentUser:Partial<userToFetch>)=>void;
}

export const userCurrentUserStore = create<currentUserStore>((set)=>({
    user:{
        userId:"",
        firstName:"",
        lastName:"",
        userName:"",
        email:"",
        phone:"",
        dateOfBirth:"",
        profilePic:"",
        isActive:false,
        gender:"",
        isEmailVerified:false,
        token:"",
        refreshToken:"",
    
    },
    setCurrentUser: (data)=>set((store)=>({user:{...store.user,...data}}))
}));