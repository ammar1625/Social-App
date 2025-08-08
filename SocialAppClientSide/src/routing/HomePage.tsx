import { NavLink, Outlet } from "react-router-dom";
import HomePageSideBar from "../components/HomePageSideBar";
import NavBar from "../components/NavBar";
import { BsSendFill } from "react-icons/bs";
//import user from "../images/user-male.png";
import male from "../images/user.jpg";
import female from "../images/woman-icon.jpg";
import { IoClose } from "react-icons/io5";
import NavigationSetter from "../components/NavigationSetter";
import { useEffect, useRef } from "react";
import { useIsOverlayVisibleStore } from "../stores/useOverLayVisibleStore";
import { useIsCommentVisibleStore } from "../stores/useIsCommentModelVisible";
import { useIsLikeVisibleStore } from "../stores/useIsLikeModelVisible";
import { useGetAllLikes } from "../hooks/useGetAllLikes";
import { useCurrentPostIdStore } from "../stores/useCurrentPostIdStore";
import { userCurrentUserStore } from "../stores/useCurrentUserStore";
import { useTargetUserIdStore } from "../stores/useTargetUserIdStrore";
import { useGetAllComments } from "../hooks/useGetAllComments";
import { useAddNewComment } from "../hooks/useAddNewComment";
import { useCommentContentStore } from "../stores/useCommentContentStore";
import { useQueryClient } from "@tanstack/react-query";
import { useAddNotification } from "../hooks/useAddNewNotification";
import { useNotificationsWebSocket } from "../hooks/useNotificationsWebsocket";

function HomePage()
{
    const queryClient = useQueryClient()
    const overlayref = useRef<HTMLDivElement>(null);
    const commentsRef = useRef<HTMLDivElement>(null);
    const likesRef = useRef<HTMLDivElement>(null);
    const commentContentRef = useRef<HTMLInputElement>(null);

    const {isOverlayVisible , setIsOverlayVisible} = useIsOverlayVisibleStore();
    const {isCommentVisible,setIsCommentVisible} = useIsCommentVisibleStore();
    const {isLikeVisible,setIsLikeVisible} = useIsLikeVisibleStore();
    const {postId}= useCurrentPostIdStore();
    const {user} = userCurrentUserStore();
    const {setUserId} = useTargetUserIdStore();
    const {commentContent,setCommentContent} = useCommentContentStore();
    const { data:likesData} = useGetAllLikes(postId);
    const {data:commentsData} = useGetAllComments(postId);
    const {data:newCommentData,mutate:mutateNewComment}  =useAddNewComment(queryClient , postId , user.userId);
    const {mutate:mutateNotification} = useAddNotification();
    
    const {sendNotification} = useNotificationsWebSocket('ws://localhost:7890/notifications',user.userId , queryClient);    

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

        useEffect(()=>{
            if(newCommentData)
            {
                clearFields([commentContentRef]);
                setCommentContent("");
                mutateNotification(
                    {
                        userId:user.userId,
                        postId:postId,
                        content:"has commented your post",
                        notificationTypeId:2
                    });
            }
        },[newCommentData]);
        
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

        
        

    return <div className="home-page-ctr">
                <NavigationSetter/>
                <NavBar/>
                 {/*this is overlay*/}
                <div ref={overlayref} className="overlay">
                    
                </div>

                {/*comments model*/}
                <div ref={commentsRef} className="comments-model invisible">

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
                        <input ref={commentContentRef}  type="text" className="comment-input" placeholder="write a comment" 
                            onChange={(e)=>{
                                //set the comment content
                                setCommentContent(e.target.value);
                            }}
                        />
                        <button className="send-comment-btn"
                            //handle comment send
                            onClick={()=>{
                                if(!commentContent)
                                {
                                    return;
                                }
                                mutateNewComment(
                                    {
                                        userId:user.userId,
                                        postId:postId,
                                        content:commentContent?commentContent:""   
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

                <div ref={likesRef} className="likes-model invisible">
                    <button className="likes-close-btn"
                        onClick={()=>{
                            setIsOverlayVisible(false);
                            setIsLikeVisible(false);
                        }}
                    ><IoClose size={22} color="gray"/></button>
                    
                   {/*  <NavLink to="#" className="liker-ctr">
                        <img src={user} alt="user" className="liker-img" title="user" />
                        <p className="liker-name">Ammar bouzenka</p>
                    </NavLink> */}
                    {likesData?.map(l=>  <NavLink key={l.likeId} to={l.userId===user.userId?"/current-user":"/user"} className="liker-ctr"
                        onClick={()=>
                            {
                                if(l.userId!==user.userId)
                                    {
                                        setUserId(l.userId);
                                    }
                                setIsOverlayVisible(false);
                                setIsCommentVisible(false);
                                setIsLikeVisible(false);
                            }}
                    >
                        <img src={l.user.profilePic?l.user.profilePic:l.user.gender==="M"?male:female} alt="user" className="liker-img" title="user" />
                        <p className="liker-name">{l.user.firstName+" "+l.user.lastName}</p>
                    </NavLink>)}
               
                </div>

                <div id="main" className="home-page-body-ctr">
                    <HomePageSideBar/>
                    <Outlet/> 
                </div>  
    </div>
}

export default HomePage;