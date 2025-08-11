import { NavLink } from "react-router-dom";
//import userPic from "../images/user-male.png";
import { FaFileImage } from "react-icons/fa6";
//import logo from "../images/social.webp";
import { MdOutlineDeleteForever } from "react-icons/md";
 import { BiLike } from "react-icons/bi"; 
import { BiSolidLike } from "react-icons/bi";
import { FaRegComment } from "react-icons/fa";
import { useGetUserAndFriendsPosts } from "../hooks/useGetUserAndFriendsPosts";
import { userCurrentUserStore } from "../stores/useCurrentUserStore";
import male from "../images/user.jpg";
import female from "../images/woman-icon.jpg"
import { useTargetUserIdStore } from "../stores/useTargetUserIdStrore";
import { useIsOverlayVisibleStore } from "../stores/useOverLayVisibleStore";
import { useIsCommentVisibleStore } from "../stores/useIsCommentModelVisible";
import { useIsLikeVisibleStore } from "../stores/useIsLikeModelVisible";
import { useCurrentPostIdStore } from "../stores/useCurrentPostIdStore";
import { useQueryClient } from "@tanstack/react-query";
import React, { useEffect, useRef, useState } from "react";
import { useAddNewPost } from "../hooks/useAddNewPost";
import { useAddNewLike } from "../hooks/useAddNewLike";
//import { useNotificationsWebSocket } from "../hooks/useNotificationsWebsocket";
//import { useIsLikeExists } from "../hooks/useIsLikeExists";
import { useNotification } from "../contexts/NotificationsContext";
import { useDeleteLike } from "../hooks/useDeleteLike";


function Posts()
{
    const queryClient = useQueryClient();
    const {user} = userCurrentUserStore();
    const {data:posts} = useGetUserAndFriendsPosts(user.userId);
    const {data:newPostData,mutate:mutateNewPost} = useAddNewPost(queryClient , user.userId);
   
    const [imgUrl , setImgUrl] = useState("");
    const [postContent , setPostContent] = useState("");
    const [selectedFile , setSelectedFile] = useState<File|null>(null);
    const {setUserId: setTargetUserId}  =useTargetUserIdStore();
    const {setIsOverlayVisible} = useIsOverlayVisibleStore();
    const {setIsCommentVisible} = useIsCommentVisibleStore();
    const {setIsLikeVisible} = useIsLikeVisibleStore();
    const {setCurrentPostId} = useCurrentPostIdStore();
    const {userId} = useTargetUserIdStore();
    const [expandedPosts, setExpandedPosts] = useState<Record<string, boolean>>({});
    //const isLikeExists = useIsLikeExists({postId:postId , userId:user.userId});
    const {mutate:mutateNewLike} = useAddNewLike(queryClient , user.userId ,userId ,1);
    const {mutate:mutateDislike} = useDeleteLike(queryClient,user.userId,userId,1);
    //const {sendNotification} = useNotificationsWebSocket('ws://localhost:7890/notifications',user.userId , queryClient);
    
    const {sendNotification} = useNotification(); // use notification context that contains sendNotification function
    const ImgInputRef = useRef<HTMLInputElement>(null);
    const postContentRef = useRef<HTMLInputElement>(null);
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
      
     function handleImageSelection(e:React.ChangeEvent<HTMLInputElement>)
     {
        if(e.target.files && e.target.files.length>0)
        {
            setSelectedFile(e.target.files[0]);
            setImgUrl(URL.createObjectURL(e.target.files[0]));
            
        }
     }

     function handleImageDeletion()
     {
        setImgUrl("");
        setSelectedFile(null);
        if(ImgInputRef.current)
        ImgInputRef.current.value="";
     }

     const togglePostExpansion = (postId: string) => {
        setExpandedPosts((prev) => ({
          ...prev,
          [postId]: !prev[postId] // Toggle value
        }));
      };

     //handle after post addition effects
     useEffect(()=>{
        if(newPostData)
        {
            if(postContentRef.current)
                postContentRef.current.value="";

            setSelectedFile(null);
            setImgUrl("");
            setPostContent("");
            if(ImgInputRef.current)
                ImgInputRef.current.value="";
        }
     },[newPostData]);
     
    return <div className="posts">

        {/*this is new post to create*/}
        <div className="new-post-ctr">
                <NavLink to="/current-user">
                    <img className="post-user-pic" src={user.profilePic?user.profilePic:user.gender==="M"?male:female}/>
                </NavLink>
                <div className="new-post-input-ctr">

                    <input ref={postContentRef} type="text" className="new-post-input" placeholder="what's new?..." onChange={(e)=>setPostContent(e.target.value)}/>
                    <input ref={ImgInputRef} id="input" type="file" className="new-post-media-input" accept="image/*" onChange={(e)=>handleImageSelection(e)} />
                    <label htmlFor="input" className="new-post-media-logo">
                        <FaFileImage color="green" size={22}/>
                    </label>
                </div>
                 
                {imgUrl&&<div className="new-post-img-ctr ">
                    <img src={imgUrl} className="post-img" />
                    <button className="delete-img-icon" onClick={handleImageDeletion}>
                        <MdOutlineDeleteForever size={27} color="#193cb8"/>
                    </button>
                 </div>}
                 

                <div className="button-ctr">
                    <button className="post-btn" onClick={()=>{
                        if(!selectedFile && !postContent)
                        {
                            return;
                        }
                        mutateNewPost({
                            userId:user.userId,
                            content:postContent?postContent:null,
                            media:selectedFile
                        });
                    }}>
                        post
                    </button>
                </div>
        </div>
        {/*this is post*/}
        {posts?.map(p=><div key={p.postId} className="new-post-ctr">
            
            <div className="post-header">
                
                    <NavLink data-user-id = {p.userId} to={p.userId===user.userId?"/current-user":"/user"} className="user-post-ctr"
                     onClick={(e)=>{
                        if(e.currentTarget.dataset.userId !== user.userId)
                        {
                            //store the target user id 
                            setTargetUserId(e.currentTarget.dataset.userId?e.currentTarget.dataset.userId:"");
                        }
                     }}
                    >
                    <img src={p.profilePic?p.profilePic:p.gender==="M"?male:female}  className="post-user-img" />
                    <p className="post-user-name">{p.firstName+" "+p.lastName}</p>
                    </NavLink>
                  
                
                <p className="post-time">{getTimeAgo(p.createdAt)}</p>
            </div>

          { p.content&& <p className="post-content">
                {!expandedPosts[p.postId]
                ? `${p.content?.slice(0, 101)}`
                : p.content}
                
               {(p.content && p.content.length>=100)&&<span className="show-more" 
                onClick={(e)=>{
                    e.stopPropagation();
                    togglePostExpansion(p.postId);
                    
                }}
               > {expandedPosts[p.postId]?"show less": "...show more"}</span>}
            </p>}

           { p.mediaUrl && <img src={p.mediaUrl} className="post-media-img" />}

            <div className="likes-comments-count-ctr" >

                <div className="likes-count-ctr" data-post-id = {p.postId}
                 onClick={(e)=>{
                    setIsOverlayVisible(true);
                    setIsLikeVisible(true);
                    setIsCommentVisible(false);
                    setCurrentPostId(e.currentTarget.dataset.postId?e.currentTarget.dataset.postId:"");
                }}
                >
                    <button className="like-icon comment-icon"> likes </button>
                    <p className="likes-count">{p.likesCount}</p>
                </div>

                <div className="comments-count-ctr" data-post-id = {p.postId} onClick={(e)=>{
                    setIsOverlayVisible(true);
                    setIsCommentVisible(true);
                    setIsLikeVisible(false);
                    setCurrentPostId(e.currentTarget.dataset.postId?e.currentTarget.dataset.postId:"");

                }}>
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
                        //capture the current post id to use it for unlike the post
                        //setCurrentPostId(p.postId);
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
                                    postId:p.postId,
                                    userId:user.userId
                                }); 
                
                                setTimeout(()=>{
                                    sendNotification(JSON.stringify({
                                        content:"has liked your post",
                                        postId:p.postId,
                                        userId:user.userId,
                                        notificationTypeId:1
                                    }));
                                },300);
                            }
                        
                    }}
                >{p.isLiked==="yes"?<BiSolidLike size={22} color="blue"/>:<BiLike size={22} color="blue"/>}</button>
                <div className="reactions-icons" data-post-id = {p.postId}
                onClick={(e)=>{
                    setIsOverlayVisible(true);
                    setIsCommentVisible(true);
                    setIsLikeVisible(false);
                    setCurrentPostId(e.currentTarget.dataset.postId?e.currentTarget.dataset.postId:"");

                }}
                ><FaRegComment size={22}/></div>
            </div>
            
        </div>)}
  
       
    </div>
}

export default Posts;