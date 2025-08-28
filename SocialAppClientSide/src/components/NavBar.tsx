import {  NavLink } from "react-router-dom";

//import pic from "../images/user-male.png";
import logo from "../images/social.webp";
import male from "../images/user.jpg";
import female from "../images/woman-icon.jpg";

import { IoNotificationsOutline } from "react-icons/io5";
import { LiaUserFriendsSolid } from "react-icons/lia";
import { userCurrentUserStore as useCurrentUserStore } from "../stores/useCurrentUserStore";
import React, { useEffect, useRef, useState } from "react";
import { useGetFilteredUsersList } from "../hooks/useGetFilteredUsersList";
import { useGetUnreadNotifications } from "../hooks/useGetAllUnreadNotifications";
import { useGetAllPendingInvitations } from "../hooks/useGetAllPendingInvitations";
import { useChangeNotificationStatus } from "../hooks/useChangeNotificationStatus";
import { useQueryClient } from "@tanstack/react-query";
import { useChangeInvitationStatus } from "../hooks/useChangeinvitationStatus";
import { useAddNewFriendship } from "../hooks/useAddNewFriendship";
import { useAddNewFriendshipMember } from "../hooks/useAddNewFriendshipMember";
import { useTargetUserIdStore } from "../stores/useTargetUserIdStrore";
import { useInvitationsWebSocket } from "../hooks/useInvitationsWebSocket";
//import { useNotificationsWebSocket } from "../hooks/useNotificationsWebsocket";
//import { invitationModel } from "../models/models";
//import { useInvitationsStore } from "../stores/useInvitationsStore";
//import { invitationModel } from "../models/models";
//import { useInvitationId } from "../stores/useInvitationId";

function NavBar()
{
    const [friendshipMemberId,setFriendshipMemberId] = useState("");
    const queryClient = useQueryClient();

    const notificationsRef = useRef<HTMLDivElement>(null);   
    const invitationsRef = useRef<HTMLDivElement>(null);   
    const {user} = useCurrentUserStore();
    const {userId,setUserId} = useTargetUserIdStore();
    //const {invitationId , setInvitationId} = useInvitationId();
    const searchInputRef = useRef<HTMLInputElement>(null);
    //const {invitations:wsInvitations,setInvitations} = useInvitationsStore();

    const [nameFilter ,setNameFilter] = useState("");

    const {data:usersData} = useGetFilteredUsersList(nameFilter);
    const {data:notifications} = useGetUnreadNotifications(user.userId);
    const {data:invitations} = useGetAllPendingInvitations(user.userId);
    const { mutate:mutateNotificationtionStatus} = useChangeNotificationStatus(queryClient,user.userId);
    const { mutate:mutateInvitationStatus}  = useChangeInvitationStatus(queryClient,user.userId , user.userId , userId , userId , user.userId);
    const {data:friendshipData , mutate :mutateFriendship} = useAddNewFriendship();
    const {mutate:mutateFriendshipMember} = useAddNewFriendshipMember();

    

   const {send:sendInvitation} =  useInvitationsWebSocket('ws://localhost:7890/invitations',user.userId , queryClient);
    
   //useNotificationsWebSocket('ws://127.0.0.1:7890/notifications',user.userId,queryClient);
    
    function displayElement(element:React.RefObject<HTMLDivElement|null>)
    {
        if(element.current)
            {
                element.current.style.opacity="100";

            } 
        element.current?.classList.remove("invisible");
    }

    function hideElement(element:React.RefObject<HTMLDivElement|null>)
    {
        if(element.current)
            {
                 element.current.style.opacity="0";

            } 
            element.current?.classList.add("invisible");
    }

     const handleinvitationMutation = (event: React.MouseEvent<HTMLButtonElement> , status:number , recieverId:string) => {
        const parentDiv = (event.target as HTMLElement).closest<HTMLDivElement>('.invitation');
      
        if (parentDiv) 
        {
          //setInvitationId(parentDiv.dataset.id?parentDiv.dataset.id:"");
          const invitationId = parentDiv.dataset.id; //
          
         mutateInvitationStatus({invitationId:invitationId? invitationId:"",
                invitationStatus:status
         });
        
         setTimeout(()=>{
            sendInvitation(JSON.stringify({
                type:status,
                senderId:user.userId,
                recieverId:recieverId
            }));
         },400);
          
        }
      }; 

    //handle add new friendship members
     useEffect(()=>{
        if(friendshipData)
        {
            //add the current user as a new friendship member
             mutateFriendshipMember(
                {friendshipId: friendshipData.friendShipId? friendshipData.friendShipId:"",
                userId:user.userId
            });

             //add the second user as new friendship member
             mutateFriendshipMember(
                {friendshipId: friendshipData.friendShipId? friendshipData.friendShipId:"",
                userId:friendshipMemberId
            }); 

            //clear the current friendship member id
            setTimeout(() => {
                setFriendshipMemberId("");
            }, 1000);
        }
    },[friendshipData]); 

   
    return <><nav className="nav-bar">

        <div className="upper-side">
             <NavLink to="/current-user" className="connected-user-ctr">
                <img className="connected-user-img" src={user.profilePic?user.profilePic:user.gender==="M"?male:female}/>
                <p className="connected-user-name">{user.userName} </p>
            </NavLink>

            <input ref={searchInputRef} type="text" className="search-input" placeholder="search for users" 
            onChange={(e)=>setNameFilter(e.target.value)}/>

            <img className="nav-bar-logo" src={logo}/>
        </div>

        <div className="down-side">
            <div className="icons-ctr">
               <button className="icon-btn" 
                onMouseEnter={()=>{displayElement(notificationsRef)}} 
                onMouseLeave={()=>hideElement(notificationsRef)}
                >
                    <IoNotificationsOutline size={28} color="#363836"/>{(notifications&&notifications.length >0) && <p className="notifications-count">{notifications?.length}</p>}</button>
                <button  className="icon-btn" 
                onMouseEnter={()=>{
                        displayElement(invitationsRef);
                }}
                onMouseLeave={()=>{
                        hideElement(invitationsRef);
                }}
                >
                    <LiaUserFriendsSolid size={28} color="#363836"/>{(invitations&&invitations.length>0)&&<p className="invitations-count">{invitations?.length}</p>}</button>
            </div>
        </div>
       

    </nav>

    {usersData&&<div className="found-users-list">
     

        {usersData?.filter(u=>u.userId!==user.userId).map(u=> <NavLink onClick={()=>setUserId(u.userId)} key={u.userId} to="/user" className="found-user">
            <img src={u.profilePic?u.profilePic:u.gender==="M"?male:female} className="found-user-img"/>
            <p className="found-user-name">{u.firstName+" "+u.lastName}</p>
        </NavLink>)}

       
    </div>}

    <div  ref={notificationsRef}  className="notifications-list invisible" 
        onMouseOver={()=>displayElement(notificationsRef)}
        onMouseLeave={()=>hideElement(notificationsRef)}
    >
        
        { notifications?.length===0?"   no notifications": notifications?.map(n=><div data-id={n.notificationId} key={n.notificationId} className="notification" onClick={(e)=>
            {
                mutateNotificationtionStatus(e.currentTarget.dataset.id)
            }}>
            <img src={n.profilePic?n.profilePic:n.gender==="M"?male:female} className="notification-user-img"/>
            <p className="notification-text"><b>{n.firstName}</b> {n.content}</p>

        </div>)}
   
    </div>

    <div  ref={invitationsRef}  className="invitations-list invisible"
        onMouseEnter={()=>displayElement(invitationsRef)}
        onMouseLeave={()=>hideElement(invitationsRef)}
    >
        

        {invitations?.length===0?"    no invitations": invitations?.map(i=><div data-id = {i.invitationId} key={i.invitationId} className="invitation">
            <NavLink data-id = {i.senderId} to="/user" className="sender"
            onClick={(e)=>setUserId(e.currentTarget.dataset.id?e.currentTarget.dataset.id:"")}
            >
                <img src={i.sender.profilePic?i.sender.profilePic:i.sender.gender==="M"?male:female} className="invitation-pic" />
                <p className="sender-name">{i.sender.firstName+" "+i.sender.lastName}</p>
            </NavLink>

            <div className="invitations-buttons-ctr">
                    <button data-user-id = {i.senderId} className="invitation-btn accept" onClick={(e)=>
                        {
                            //accept the invitation
                            handleinvitationMutation(e,2,i.senderId);

                            setFriendshipMemberId(e.currentTarget.dataset.userId? e.currentTarget.dataset.userId:"");
                            
                            //add friendship
                            mutateFriendship();
                        }}>accept</button>
                    <button  className="invitation-btn reject" onClick={(e)=>
                        {
                            //refuse the invitation
                            handleinvitationMutation(e,3,i.senderId);
                        }}>reject</button>
            </div>
        </div>)}

    </div>

    </> 
}

export default NavBar;