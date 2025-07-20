import {  NavLink, useNavigate } from "react-router-dom";
//import user from "../images/user-male.png";
import male from "../images/user.jpg";
import female from "../images/woman-icon.jpg";
import { BiLike, BiSolidLike , BiSolidMessageRoundedDetail } from "react-icons/bi";
import { FaRegComment, FaUserFriends } from "react-icons/fa";
import { IoClose, IoPersonAdd } from "react-icons/io5";
import { RiUserReceivedFill , RiUserSharedFill } from "react-icons/ri";
import { useTargetUserIdStore } from "../stores/useTargetUserIdStrore";
import { useGetUserById } from "../hooks/useGetUserById";
import { useGetAllFriends } from "../hooks/useGetAllFriends";
import { useGetCurrentUserPosts } from "../hooks/useGetCurrentUserPosts";
import { userCurrentUserStore } from "../stores/useCurrentUserStore";
//import User from "../images/user-male.png"
import { useEffect, useRef, useState } from "react";
import { useIsOverlayVisibleStore } from "../stores/useOverLayVisibleStore";
import { useIsCommentVisibleStore } from "../stores/useIsCommentModelVisible";
import { useIsLikeVisibleStore } from "../stores/useIsLikeModelVisible";
import { BsSendFill } from "react-icons/bs";
import { useCurrentPostIdStore } from "../stores/useCurrentPostIdStore";
import { useGetAllLikes } from "../hooks/useGetAllLikes";
import { useGetAllComments } from "../hooks/useGetAllComments";
import { useIsFriendshipExists } from "../hooks/useIsFriendshipExsits";
import { useIsInvitationExists } from "../hooks/useIsInVitationExists";
//import { useAddNewInvitation } from "../hooks/useAddNewInvitation";
import { useQueryClient } from "@tanstack/react-query";
import { useCancelInvitation } from "../hooks/useCancelInvitation";
import { useAddNewFriendship } from "../hooks/useAddNewFriendship";
import { useAddNewFriendshipMember } from "../hooks/useAddNewFriendshipMember";
import { useChangeInvitationStatus } from "../hooks/useChangeinvitationStatus";
import { useAddNewComment } from "../hooks/useAddNewComment";
import { useAddNotification } from "../hooks/useAddNewNotification";
import { useCommentContentStore } from "../stores/useCommentContentStore";
import { useInvitationsWebSocket } from "../hooks/useInvitationsWebSocket";
import { useNotificationsWebSocket } from "../hooks/useNotificationsWebsocket";
import { useAddNewLike } from "../hooks/useAddNewLike";
import { useDeleteLike } from "../hooks/useDeleteLike";
import { useGetConversationByMembers } from "../hooks/useGetConversationByMembers";
import { useAddNewConversation } from "../hooks/useAddNewConversation";
import { useAddNewConversationMember } from "../hooks/useAddNewConversationMember";
//import { useInvitationsStore } from "../stores/useInvitationsStore";
function UserProfile()
{
    const queryClient = useQueryClient();
    const navigate = useNavigate();
    const {user} = userCurrentUserStore();
    const {userId ,setUserId}= useTargetUserIdStore();
    const {data:userData} = useGetUserById(userId);
    const {data:friends} = useGetAllFriends(userId);
    const {data:posts} = useGetCurrentUserPosts({userId:userId , currentUserId:user.userId});

    const overlayref = useRef<HTMLDivElement>(null);
    const commentsRef = useRef<HTMLDivElement>(null);
    const likesRef = useRef<HTMLDivElement>(null);
    const friendshipResponseRef = useRef<HTMLDivElement>(null);
    const cancelRequestRef = useRef<HTMLDivElement>(null);
    const commentInputRef = useRef<HTMLInputElement>(null);

    const {isOverlayVisible,setIsOverlayVisible} = useIsOverlayVisibleStore();
    const {isCommentVisible,setIsCommentVisible} = useIsCommentVisibleStore();
    const {isLikeVisible,setIsLikeVisible} = useIsLikeVisibleStore();
    const {postId,setCurrentPostId} = useCurrentPostIdStore();
    const {commentContent,setCommentContent} = useCommentContentStore();
    const [expandedPosts, setExpandedPosts] = useState<Record<string, boolean>>({});
    
    //const {invitations:wsInvitations ,setInvitations} = useInvitationsStore();
   // const {isSent , setIsSent} = useState<boolean>(false);
    //const { userId} = useTargetUserIdStore();
    const {data:likesData} = useGetAllLikes(postId);
    const {data:commentsData} = useGetAllComments(postId);
    const {data:isFriendshipExistsData} = useIsFriendshipExists(user.userId,userId);
    const {data:isInvitationRecievedData} = useIsInvitationExists(userId,user.userId);
    const {data:isInvitationSentData }  =useIsInvitationExists(user.userId,userId);
    //const {mutate:mutateNewInvitation} = useAddNewInvitation(queryClient , user.userId,userId);
    const {mutate:mutateCancelInvitation} = useCancelInvitation(queryClient,user.userId,userId);
    const {data:newFriendshipData , mutate:mutateFriendship} = useAddNewFriendship();
    const { mutate:mutateFriendshipMember} = useAddNewFriendshipMember();
    const {mutate:mutateInvitationStatus}  = useChangeInvitationStatus(queryClient,user.userId , user.userId,userId , userId , user.userId);
    const {data:newCommentData , mutate:mutateComment}  = useAddNewComment(queryClient,postId,user.userId);
    const { mutate:mutateNotification} = useAddNotification();
    const {send:sendInvitation} = useInvitationsWebSocket('ws://localhost:7890/invitations',user.userId , queryClient);
    const {sendNotification} = useNotificationsWebSocket('ws://localhost:7890/notifications',user.userId , queryClient);    
    const {mutate:mutateNewLike} = useAddNewLike(queryClient , user.userId ,userId, 2);
    const {mutate:mutateDislike} = useDeleteLike(queryClient , user.userId ,userId, 2);
    const {data:conversationData} = useGetConversationByMembers({currentUserId:user.userId , targetUserId:userId});
    const {data:newConversationData, mutate:mutateConversation}  =useAddNewConversation();
    const {mutateAsync:mutateConversationMemberAsync} = useAddNewConversationMember();
   

    console.log("is conversation exists ",conversationData);
    if(newConversationData)
    {
     console.log("new conversation data ",newConversationData);
    }
    function displayElement(element:React.RefObject<HTMLDivElement|null> , opacity:string)
            {
                if(element.current)
                    {
                        element.current.style.opacity=opacity;
        
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

            function clearFields(fields:React.RefObject<HTMLInputElement|null>[])
            {
                fields.forEach((field)=>{
                    if(field.current)
                    {
                        field.current.value = "";
                        field.current.classList.remove("invalid-input-field");
                    }
                });
            }

            const togglePostExpansion = (postId: string) => {
                setExpandedPosts((prev) => ({
                  ...prev,
                  [postId]: !prev[postId] // Toggle value
                }));
              };

              const handleAddConversationMembers = async () => {
                try {
                    await Promise.all([
                        mutateConversationMemberAsync({conversationId:newConversationData?.conversationId?newConversationData.conversationId:"",userId:user.userId}),
                        mutateConversationMemberAsync({conversationId:newConversationData?.conversationId?newConversationData.conversationId:"",userId:userId})
                    ]);
            
                    navigate("/conversation");
                } catch (error) {
                    console.error("Error creating members:", error);
                }
            };
    
            //handle comments model display and hide
             useEffect(()=>{
                if(isOverlayVisible && isCommentVisible)
                {
                    displayElement(overlayref,"50%");
                    displayElement(commentsRef,"100%");
                }
                else if(!isOverlayVisible && !isCommentVisible)
                {
                    hideElement(overlayref);
                    hideElement(commentsRef);
                }
            },[isCommentVisible,isOverlayVisible]); 
    
            //handle likes model display and hide
            useEffect(()=>{
                if(isOverlayVisible&&isLikeVisible)
                {
                    displayElement(overlayref,"60%");
                    displayElement(likesRef,"100%");
                }
                else if(!isOverlayVisible&&!isLikeVisible)
                {
                    hideElement(overlayref);
                    hideElement(likesRef);
                }
            },[isOverlayVisible,isLikeVisible]);


            //handle add new friensdhip member
            useEffect(()=>{
                if(newFriendshipData)
                {
                    mutateFriendshipMember(
                        {
                            friendshipId:newFriendshipData.friendShipId? newFriendshipData.friendShipId:"",
                            userId:user.userId
                    });

                    mutateFriendshipMember(

                        {
                            friendshipId:newFriendshipData.friendShipId? newFriendshipData.friendShipId:"",
                            userId:userId
                        }
                    );

                }
            },[newFriendshipData]);

            //hande add new notification
            useEffect(()=>{
                if(newCommentData)
                {
                    clearFields([commentInputRef]);
                    setCommentContent("");
                    mutateNotification({
                        postId:postId,
                        userId:user.userId,
                        content:"has commented your post",
                        notificationTypeId:2
                    });
                }
            },[newCommentData]);

            //handle new conversation members addition
            useEffect(()=>{
                if(newConversationData)
                {
                   
                    const runCreation = async ()=>{
                        await handleAddConversationMembers();
                    }
                    
                    runCreation();
                }
            },[newConversationData])

    
    function getTimeAgo(dateString: string): string {
        const date = new Date(dateString);
        const now = new Date();
      
        const diffMs = now.getTime() - date.getTime(); // difference in milliseconds
        const diffSeconds = Math.floor(diffMs / 1000);
        const diffMinutes = Math.floor(diffSeconds / 60);
        const diffHours = Math.floor(diffMinutes / 60);
        const diffDays = Math.floor(diffHours / 24);
        const diffWeeks = Math.floor(diffDays / 7);
      
        if (diffSeconds < 60) {
          return "now";
        } else if (diffMinutes < 60) {
          return `${diffMinutes}m`;
        } else if (diffHours < 24) {
          return `${diffHours}h`;
        } else if (diffDays <= 7) {
          return `${diffDays}d`;
        } else {
          return `${diffWeeks}w`;
        }
      }

      function showElement(element:React.RefObject<HTMLDivElement|null>)
          {
              if(element.current)
                  {
                      element.current.style.opacity="100";
      
                  } 
              element.current?.classList.remove("invisible");
          }
      
         
    return <div className="profile-ctr">
         {/*this is overlay*/}
         <div ref={overlayref} className="overlay">
                    
                    </div>
    
                    {/*comments model*/}
                    <div ref={commentsRef} className="comments-model ">
    
                      { commentsData?.map (c=><div key={c.commentId} className="comment-ctr">
                            <NavLink to={c.userId===user.userId?"/current-user":"/user"} className="comment-user"
                                onClick={()=>{
                                    if(c.userId!==user.userId)
                                    {
                                        setUserId(c.userId);
                                    }
                                    setIsOverlayVisible(false);
                                    setIsCommentVisible(false);
                                    setIsLikeVisible(false);
                                }}
                            >
                            <img src={c.user.profilePic?c.user.profilePic:c.user.gender==="M"?male:female} alt="user pic" title="user" className="comment-user-pic" />

                            </NavLink>
                           
                            <div className="comment-content-ctr">
                                <p className="commenter-name">{c.user.firstName+" "+c.user.lastName}</p>
                                <p className="comment-content">{c.content}</p>
                            </div>
                            <p className="comment-time">{getTimeAgo(c.sentAt)}</p>
                        </div>)}
    
                        
    
                        <div className="comment-input-ctr">
                            <input ref={commentInputRef} type="text" className="comment-input" placeholder="write a comment" 
                                onChange={(e)=>{
                                    setCommentContent(e.target.value);
                                }}
                            />
                            <button className="send-comment-btn"
                                onClick={()=>{
                                    if(!commentContent)
                                    {
                                        return;
                                    }
                                    mutateComment({
                                        content:commentContent?commentContent:"",
                                        postId:postId,
                                        userId:userId
                                    });

                                    setTimeout(()=>{
                                        sendNotification(JSON.stringify({
                                            content:"has commented your post",
                                            postId:postId,
                                            userId:user.userId,
                                            notificationTypeId:2
                                        }));
                                    },300);
                                }}
                            ><BsSendFill size={22} color="blue"/></button>
                        </div>
    
                    <button className="comments-close-btn" 
                        onClick={()=>{
                            setIsOverlayVisible(false);
                            setIsCommentVisible(false);
                        }}
                    ><IoClose size={22} color="gray"/></button>
    
                    </div>
    
                    <div  ref={likesRef} className="likes-model invisible">
                        <button className="likes-close-btn"
                            onClick={()=>{
                                setIsOverlayVisible(false);
                                setIsLikeVisible(false);
                            }}
                        ><IoClose size={22} color="gray"/></button>
                       {/*  
                        <NavLink to="#" className="liker-ctr">
                            <img src={User} alt="user" className="liker-img" title="user" />
                            <p className="liker-name">Ammar bouzenka</p>
                        </NavLink> */}
                        {likesData?.map(l=><NavLink key={l.likeId} to={l.userId===user.userId?"/current-user":"/user"} className="liker-ctr"
                            onClick={()=>{
                                if(l.userId!==user.userId)
                                    {
                                     setUserId(l.userId);
                                    }
                                setIsOverlayVisible(false);
                                setIsLikeVisible(false);
                                setIsCommentVisible(false);
                                
                            }}
                        >
                            <img src={l.user.profilePic?l.user.profilePic:l.user.gender==="M"?male:female} alt="user" className="liker-img" title="user" />
                            <p className="liker-name">{l.user.firstName+" "+l.user.lastName}</p>
                        </NavLink>)}
                    </div>

                    {/*this friendship request reply div*/}
                    {isInvitationRecievedData && <div ref={friendshipResponseRef} className="friendship-request-response-ctr invisible"
                        onMouseEnter={()=>{
                            showElement(friendshipResponseRef);
                        }}
                        onMouseLeave={()=>
                        {
                            hideElement(friendshipResponseRef);
                        }
                        }
                    >
                            <button className="response-btn" 
                                onClick={()=>{
                                    // add new friendship
                                    mutateFriendship();
                                    mutateInvitationStatus(
                                        {
                                            invitationId:isInvitationRecievedData?.invitationId?isInvitationRecievedData.invitationId:"",
                                            invitationStatus:2
                                        }
                                    );

                                    //send invitation reply through web socket to inform the other client that the request has been accepted
                                    setTimeout(() => {
                                        sendInvitation(JSON.stringify({
                                            type:2,
                                            senderId:user.userId,
                                            recieverId:isInvitationRecievedData.senderId
                                        }));
                                    }, 300);
                                }}
                            >accept</button>
                            <button className="response-btn"
                                onClick={()=>{
                                    //set the invitation rejected
                                    mutateInvitationStatus(
                                        {
                                            invitationId:isInvitationRecievedData?.invitationId?isInvitationRecievedData.invitationId:"",
                                            invitationStatus:3
                                        });

                                    //send invitation reply through web socket to inform the other client that the request has been rejecetd
                                        setTimeout(() => {
                                            sendInvitation(JSON.stringify({
                                                type:3,
                                                senderId:user.userId,
                                                recieverId:isInvitationRecievedData.senderId
                                            }));
                                        }, 300);
                                }}
                            >reject</button>
                    </div>}

                      {/*this is the invitation cancel div*/}
                     {isInvitationSentData&& <div ref={cancelRequestRef} className="friendship-request-cancel-ctr invisible"
                        onMouseEnter={()=>{
                            showElement(cancelRequestRef);
                        }}

                        onMouseLeave={()=>{
                            hideElement(cancelRequestRef);
                        }}
                      >
                            <button className="friendship-request-cancel-btn" 
                            //cancel invitation
                            onClick={()=>{
                                mutateCancelInvitation({
                                    type:1,
                                    senderId:user.userId,
                                    recieverId:userId
                                });

                               //send invitation reply through web socket to inform the other client that the request has been canceled
                                setTimeout(() => {
                                    sendInvitation(JSON.stringify({
                                        type:4,
                                        senderId:user.userId,
                                        recieverId:isInvitationSentData?.recieverId
                                    }));
                                }, 300);
                            }}
                            >cancel</button>
                           
                        </div>}
        {/*this is user profile*/}
        <div className="profile">
            <div className="infos-ctr">
                <img src={userData?.profilePic?userData.profilePic:userData?.gender==="M"?male:female} alt="user" className="profile-pic" />
                <p className="user-name">{userData?.firstName+" "+userData?.lastName}</p>
               
            </div>
            <div className="button-ctr">
                <button onMouseEnter={()=>{
                        if(isInvitationRecievedData)
                        {
                            showElement(friendshipResponseRef);
                           
                        }

                        else if(isInvitationSentData)
                        {
                            showElement(cancelRequestRef);
                           
                        }
                       
                    }}

                    onMouseLeave={()=>
                    {
                        hideElement(cancelRequestRef);
                        hideElement(friendshipResponseRef);
                    }}
                    
                    onClick={()=>{
                        //if the user is not a friend nor has sent an invitation or the current user has sent him an invitation then send him an invitation
                        if(!isFriendshipExistsData && !isInvitationRecievedData && !isInvitationSentData)
                        {
                           
                            sendInvitation(JSON.stringify({
                                type:1,
                                senderId:user.userId,
                                recieverId:userId
                            }));
                             setTimeout(() => {
                                queryClient.invalidateQueries({queryKey:["isInvitationExists",user.userId,userId]});
                                
                            }, 200);
                            
                        }

                       
                    }}

                    className="add-friend-btn">{isFriendshipExistsData?<>friend  <FaUserFriends size={15} color="white"/></> : isInvitationRecievedData? <>reply <RiUserReceivedFill size={15} color="white"/></> : isInvitationSentData? <>request sent <RiUserSharedFill size={15} color="white"/> </> : <>add friend  <IoPersonAdd size={15} color="white"/></>}</button>
                <button className="add-friend-btn"
                    //handle conversation addition
                    onClick={()=>{
                        if(!conversationData)//if there is not existing conversation with this user then create one
                        {
                            mutateConversation();
                        }
                        else //otherwise navigate directly to the existing conversation
                        {
                            navigate("/conversation");
                        }
                    }}
                
                ><>message <BiSolidMessageRoundedDetail size={15} color="white"/></></button>
            </div>
            {/*friends section*/}
            <div className="friends-ctr">
                <div className="friends-count-ctr">
                    <p className="friends-title">Friends</p>
                    <p className="friends-count">{friends?.length+" friends"}</p>
                </div>

                <div className="friends">
                    {/* <div className="friend">
                        <img src={user} alt="friend" className="friend-pic" />
                        <p className="friend-name">Iyed</p>
                    </div> */}
                    {friends?.map(f=><NavLink key={f.friendShipMemberId} data-id = {f.userId} to={f.userId===user.userId?"/current-user":"/user"} className="friend">
                        <img src={f.profilePic?f.profilePic:f.gender==="M"?male:female} alt="friend" className="friend-pic" />
                        <p className="friend-name">{f.firstName}</p>
                    </NavLink>)}

                    
                </div>
            </div>

            

        <div className="profile-posts-ctr">

        {posts?.map(p=><div key={p.postId} className="new-post-ctr profile-post-ctr">

        <div className="post-header">
            
                <NavLink to={p.userId===user.userId?"/current-user":"/user"} className="user-post-ctr profile-user-post-ctr">
                <img src={p.profilePic?p.profilePic:p.gender==="M"?male:female}  className="post-user-img profile-post-user-img" />
                <p className="post-user-name profile-post-user-name">{p.firstName+" "+p.lastName}</p>
                </NavLink>
            
            
            <p className="post-time">{getTimeAgo(p.createdAt)}</p>
        </div>

        {p.content&&<p className="post-content">
            {!expandedPosts[p.postId]?p.content?.slice(0,101):p.content} 
        {p.content.length>=100 &&<span className="show-more"
            onClick={(e)=>{
                e.stopPropagation();
                togglePostExpansion(p.postId);
            }}
        > {expandedPosts[p.postId]?"show less":"...show more"}</span>}
        </p>}

        {p.mediaUrl && <img src={p.mediaUrl} className="post-media-img profile-post-media" />}

        <div className="likes-comments-count-ctr">
            <div className="likes-count-ctr">
                <button className="comment-icon" data-post-id = {p.postId}
                    onClick={(e)=>{
                        setIsOverlayVisible(true);
                        setIsLikeVisible(true);
                        setIsCommentVisible(false);
                        setCurrentPostId(e.currentTarget.dataset.postId?e.currentTarget.dataset.postId:"");

                    }}
                > {/* <BiSolidLike size={18} color="blue"/> */}likes </button>
                <p className="likes-count">{" "}{p.likesCount}</p>
            </div>

            <div className="comments-count-ctr">
                <button className="comment-icon" data-post-id = {p.postId}
                    onClick={(e)=>{
                        setIsOverlayVisible(true);
                        setIsCommentVisible(true);
                        setIsLikeVisible(false);
                        setCurrentPostId(e.currentTarget.dataset.postId?e.currentTarget.dataset.postId:"");

                    }}
                >{/* <FaRegComment size={18}/> */}comments </button>
                <p className="comments-count" data-post-id = {p.postId}
                      onClick={(e)=>{
                        setIsOverlayVisible(true);
                        setIsCommentVisible(true);
                        setIsLikeVisible(false);
                        setCurrentPostId(e.currentTarget.dataset.postId?e.currentTarget.dataset.postId:"");

                    }}
                >{p.commentsCount}</p>
            </div>
        </div>

        <div className="like-comment-ctr">
            <button className="reactions-icons"
                onClick={()=>{
                    if(p.isLiked==='yes')
                    {
                        mutateDislike({
                            postId:p.postId,
                            userId:user.userId
                        });
                    }
                    else
                    {
                        mutateNewLike({
                            userId:user.userId,
                            postId:p.postId
                        });

                        setTimeout(() => {
                            sendNotification(JSON.stringify({
                                        content:"has liked your post",
                                        postId:p.postId,
                                        userId:user.userId,
                                        notificationTypeId:1
                            }
                            ));
                        }, 150);
                    }
                }}
            >{p.isLiked==='yes'?<BiSolidLike size={19} color="blue"/>:<BiLike size={19} color="blue"/>}</button>

            <div className="reactions-icons" data-post-id = {p.postId}
               onClick={(e)=>{
                setIsOverlayVisible(true);
                setIsCommentVisible(true);
                setIsLikeVisible(false);
                setCurrentPostId(e.currentTarget.dataset.postId?e.currentTarget.dataset.postId:"");

            }}
            ><FaRegComment size={19}/></div>
        </div>

        </div>)}
            </div>
        </div>
    </div>
}

export default UserProfile;