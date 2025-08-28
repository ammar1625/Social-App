import { NavLink } from "react-router-dom";
import male from "../images/user.jpg"
import female from "../images/woman-icon.jpg"
import { FaCamera, FaRegComment } from "react-icons/fa";
import { BiLike, BiSolidLike } from "react-icons/bi";
import { userCurrentUserStore } from "../stores/useCurrentUserStore";
import { useGetAllFriends } from "../hooks/useGetAllFriends";
import { useGetCurrentUserPosts } from "../hooks/useGetCurrentUserPosts";
import React, { useEffect, useRef, useState } from "react";
import { useIsOverlayVisibleStore } from "../stores/useOverLayVisibleStore";
import { useIsCommentVisibleStore } from "../stores/useIsCommentModelVisible";
import { useIsLikeVisibleStore } from "../stores/useIsLikeModelVisible";

//import User from "../images/user.jpg";
import { IoClose } from "react-icons/io5";
import { BsSendFill } from "react-icons/bs";
import { useCurrentPostIdStore } from "../stores/useCurrentPostIdStore";
import { useGetAllLikes } from "../hooks/useGetAllLikes";
import { useTargetUserIdStore } from "../stores/useTargetUserIdStrore";
import { useGetAllComments } from "../hooks/useGetAllComments";
import { useCommentContentStore } from "../stores/useCommentContentStore";
import { useAddNewComment } from "../hooks/useAddNewComment";
import { useQueryClient } from "@tanstack/react-query";
import { useAddNotification } from "../hooks/useAddNewNotification";
import { useAddNewLike } from "../hooks/useAddNewLike";
import { useDeleteLike } from "../hooks/useDeleteLike";
import { useChangeProfilePic } from "../hooks/useChangeProfilePic";
import NavigationSetter from "../components/NavigationSetter";
function CurrentUserProfile()
{
    const queryClient = useQueryClient();
    const {user , setCurrentUser} = userCurrentUserStore();

    const {data:friendsList}  =useGetAllFriends(user.userId);
    const {data:currentUserPosts} = useGetCurrentUserPosts({userId:user.userId , currentUserId:user.userId});
    const {data:changeProfilePicData , mutateAsync:mutateProfilePicAsync}  = useChangeProfilePic();

        const overlayref = useRef<HTMLDivElement>(null);
        const commentsRef = useRef<HTMLDivElement>(null);
        const likesRef = useRef<HTMLDivElement>(null);
        const commentInputRef = useRef<HTMLInputElement>(null);
        const picInputRef = useRef<HTMLInputElement>(null);
    
        const {isOverlayVisible,setIsOverlayVisible} = useIsOverlayVisibleStore();
        const {isCommentVisible,setIsCommentVisible} = useIsCommentVisibleStore();
        const {isLikeVisible,setIsLikeVisible} = useIsLikeVisibleStore();
        const {commentContent,setCommentContent}  = useCommentContentStore();
         const [expandedPosts, setExpandedPosts] = useState<Record<string, boolean>>({});

        const {postId,setCurrentPostId} = useCurrentPostIdStore();
        const {setUserId} = useTargetUserIdStore();
        const {data:likesData} = useGetAllLikes(postId);
        const {data:commentsData} = useGetAllComments(postId);
        const {data:newCommentData , mutate:mutateComment} = useAddNewComment(queryClient , postId,user.userId);
        const {mutate:mutateNotification} = useAddNotification();
        const {mutate:mutateLike} = useAddNewLike(queryClient, user.userId , user.userId,2);
        const {mutate:mutateDislike} = useDeleteLike(queryClient,user.userId , user.userId,2);

        const [selectedFile,setSelectedFile]  =useState<File|null>(null);
        const [imageUrl,setImageUrl] = useState("");

        function handleImageSelection(e:React.ChangeEvent<HTMLInputElement>)
        {
            if(e.target.files)
            {
                if(e.target.files?.length>0)
                    {
                        const file = e.target.files[0];
                        setSelectedFile(file);
                        setImageUrl(URL.createObjectURL(file));
                        
                    }
            }
               
        }

        function handleImageDeletion()
        {
            if(picInputRef.current)
            {
                picInputRef.current.value = "";
                setSelectedFile(null);
                setImageUrl("");
            }
        } 

        function clearImageInput()
        {
            if(picInputRef.current)
            {
                picInputRef.current.value ="";
                setSelectedFile(null);
            }
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
        
                const togglePostExpansion = (postId: string) => {
                    setExpandedPosts((prev) => ({
                      ...prev,
                      [postId]: !prev[postId] // Toggle value
                    }));
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

                //handle notification add
                useEffect(()=>{
                    if(newCommentData)
                    {
                        clearFields([commentInputRef]);
                        setCommentContent("");
                        mutateNotification({
                            userId:user.userId,
                            postId:postId,
                            content:"has commented your post",
                            notificationTypeId:2
                        });
                    }
                },[newCommentData]);


                //handle user profile pic update for the current user
                useEffect(()=>{
                    if(changeProfilePicData)
                    {
                        setCurrentUser({
                            profilePic:changeProfilePicData
                        });
                    }
                },[changeProfilePicData]);

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
    return <div className="profile-ctr">
            <NavigationSetter/>
           {/*this is overlay*/}
                 <div ref={overlayref} className="overlay">
                            
                            </div>
            
                            {/*comments model*/}
                            <div ref={commentsRef} className="comments-model ">
            
                             {  commentsData?.map (c=><div key={c.commentId} className="comment-ctr">
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
                                                postId:postId,
                                                userId:user.userId,
                                                content:commentContent,
                                            });
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
            
                            <div ref={likesRef} className="likes-model invisible">
                                <button className="likes-close-btn"
                                    onClick={()=>{
                                        setIsOverlayVisible(false);
                                        setIsLikeVisible(false);
                                    }}
                                ><IoClose size={22} color="gray"/></button>
                                
                           {/*      <NavLink to="#" className="liker-ctr">
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

        <div className="profile">
            <div className="infos-ctr">
                <img src={imageUrl?imageUrl: user.profilePic?user.profilePic:user.gender==="M"?male:female} alt="user" className="profile-pic" />
                <p className="user-name">{user.firstName+" "+user.lastName}</p>
                <input ref={picInputRef} id= "pic-id" type="file" accept="image/*" className="hidden" onChange={handleImageSelection}/>
                <label htmlFor="pic-id" className="change-pic-icon"><FaCamera size={19} color="gray"/></label>
            </div>
           <div className={selectedFile?"change-pic-dialog":"change-pic-dialog invisible"}>
                <button className="save-button"
                    onClick={async()=>{

                       await mutateProfilePicAsync({
                            userId:user.userId,
                            profilePic:selectedFile
                        });

                        clearImageInput();
                    }}
                >save</button>
                <button className="cancel-button"
                    onClick={()=>handleImageDeletion()}
                >cancel</button>
            </div>
            <div className="friends-ctr">
                <div className="friends-count-ctr">
                    <p className="friends-title">Friends</p>
                    <p className="friends-count">{friendsList?.length+" friends"}</p>
                </div>

                <div className="friends">
                    
                    {friendsList?.map(f=><NavLink key={f.friendShipMemberId} to="/user" className="friend"
                        onClick={()=>{
                            setUserId(f.userId);
                        }}
                    >
                        <img src={f.profilePic?f.profilePic:f.gender==="M"?male:female} alt="friend" className="friend-pic" />
                        <p className="friend-name">{f.firstName}</p>
                    </NavLink>)}
 
                </div>
            </div>

        <div className="profile-posts-ctr">
        
      { currentUserPosts?.map(p=> <div key={p.postId} className="new-post-ctr profile-post-ctr">

        <div className="post-header">
            
                <NavLink to="#" className="user-post-ctr profile-user-post-ctr">
                <img src={p.profilePic?p.profilePic:p.gender==="M"?male:female}  className="post-user-img profile-post-user-img" />
                <p className="post-user-name profile-post-user-name">{p.firstName+" "+p.lastName}</p>
                </NavLink>
            
            
            <p className="post-time">{getTimeAgo(p.createdAt)}</p>
        </div>

        {p.content&&<p className="post-content">
           {!expandedPosts[p.postId]?p.content.slice(0,101):p.content}     
        {p.content.length>=100&&<span className="show-more"
            onClick={(e)=>{
                e.stopPropagation();
                togglePostExpansion(p.postId);
            }}
        > {expandedPosts[p.postId]?"show less":"...show more"}</span>}
        </p>}

        {p.mediaUrl&&<img src={p.mediaUrl} className="post-media-img profile-post-media" />}

        <div className="likes-comments-count-ctr">
            <div className="likes-count-ctr">
                <button className="comment-icon" data-post-id = {p.postId}
                    onClick={(e)=>{
                        setIsOverlayVisible(true);
                        setIsCommentVisible(false);
                        setIsLikeVisible(true);
                        setCurrentPostId(e.currentTarget.dataset.postId?e.currentTarget.dataset.postId:"");

                     }}
                > {/* <BiSolidLike size={18} color="blue"/> */} likes </button>
                <p className="likes-count">{p.likesCount}</p>
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
                        mutateLike({
                            userId:user.userId,
                            postId:p.postId
                        });
                    }
                }}
            >{p.isLiked==='yes'? <BiSolidLike size={19} color="blue"/>:<BiLike size={19} color="blue"/>}</button>
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

export default CurrentUserProfile;