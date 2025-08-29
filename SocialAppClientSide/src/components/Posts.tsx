import { NavLink } from "react-router-dom";
import { FaFileImage } from "react-icons/fa6";
import { MdOutlineDeleteForever } from "react-icons/md";
import { BiLike, BiSolidLike } from "react-icons/bi";
import { FaRegComment } from "react-icons/fa";
import { useGetUserAndFriendsPosts } from "../hooks/useGetUserAndFriendsPosts";
import { userCurrentUserStore } from "../stores/useCurrentUserStore";
import male from "../images/user.jpg";
import female from "../images/woman-icon.jpg";
import { useTargetUserIdStore } from "../stores/useTargetUserIdStrore";
import { useIsOverlayVisibleStore } from "../stores/useOverLayVisibleStore";
import { useIsCommentVisibleStore } from "../stores/useIsCommentModelVisible";
import { useIsLikeVisibleStore } from "../stores/useIsLikeModelVisible";
import { useCurrentPostIdStore } from "../stores/useCurrentPostIdStore";
import { useQueryClient } from "@tanstack/react-query";
import React, { useEffect, useRef, useState } from "react";
import { useAddNewPost } from "../hooks/useAddNewPost";
import { useAddNewLike } from "../hooks/useAddNewLike";
import { useNotification } from "../contexts/NotificationsContext";
import { useDeleteLike } from "../hooks/useDeleteLike";

function Posts() {
  const queryClient = useQueryClient();
  const { user } = userCurrentUserStore();
  const { data: posts } = useGetUserAndFriendsPosts(user.userId);
  const {data: newPostData, mutate: mutateNewPost } = useAddNewPost(queryClient, user.userId);

  const [imgUrl, setImgUrl] = useState("");
  const [postContent, setPostContent] = useState("");
  const [selectedFile, setSelectedFile] = useState<File | null>(null);
  const { setUserId: setTargetUserId } = useTargetUserIdStore();
  const { setIsOverlayVisible } = useIsOverlayVisibleStore();
  const { setIsCommentVisible } = useIsCommentVisibleStore();
  const { setIsLikeVisible } = useIsLikeVisibleStore();
  const { setCurrentPostId } = useCurrentPostIdStore();
  const [expandedPosts, setExpandedPosts] = useState<Record<string, boolean>>({});

  const { mutate: mutateNewLike } = useAddNewLike(queryClient, user.userId, user.userId, 1);
  const { mutate: mutateDislike } = useDeleteLike(queryClient, user.userId, user.userId, 1);
  const { sendNotification } = useNotification();

  const ImgInputRef = useRef<HTMLInputElement>(null);
  const textareaRef = useRef<HTMLTextAreaElement>(null);

  // Auto-resize textarea on content change
  useEffect(() => {
    if (textareaRef.current) {
      textareaRef.current.style.height = 'auto';
      textareaRef.current.style.height = `${textareaRef.current.scrollHeight}px`;
    }
  }, [postContent]);

  // Reset form after successful post
  useEffect(() => {
    if (newPostData) {
      setPostContent("");
      setSelectedFile(null);
      setImgUrl("");
      if (textareaRef.current) textareaRef.current.style.height = 'auto';
      if (ImgInputRef.current) ImgInputRef.current.value = "";
    }
  }, [newPostData]);

  function getTimeAgo(dateString: string): string {
    const date = new Date(dateString);
    const now = new Date();
    const diffMs = now.getTime() - date.getTime();
    const diffSeconds = Math.floor(diffMs / 1000);
    const diffMinutes = Math.floor(diffSeconds / 60);
    const diffHours = Math.floor(diffMinutes / 60);
    const diffDays = Math.floor(diffHours / 24);
    const diffWeeks = Math.floor(diffDays / 7);

    if (diffSeconds < 60) return "now";
    if (diffMinutes < 60) return `${diffMinutes}m`;
    if (diffHours < 24) return `${diffHours}h`;
    if (diffDays <= 7) return `${diffDays}d`;
    return `${diffWeeks}w`;
  }

  function handleImageSelection(e: React.ChangeEvent<HTMLInputElement>) {
    if (e.target.files && e.target.files.length > 0) {
      setSelectedFile(e.target.files[0]);
      setImgUrl(URL.createObjectURL(e.target.files[0]));
    }
  }

  function handleImageDeletion() {
    setImgUrl("");
    setSelectedFile(null);
    if (ImgInputRef.current) ImgInputRef.current.value = "";
  }

  const togglePostExpansion = (postId: string) => {
    setExpandedPosts(prev => ({
      ...prev,
      [postId]: !prev[postId]
    }));
  };

  return (
    <div className="posts">
      {/* New Post Form */}
      <div className="new-post-ctr">
        <NavLink to="/current-user">
          <img
            className="post-user-pic"
            src={user.profilePic ? user.profilePic : user.gender === "M" ? male : female}
            alt="Your profile"
          />
        </NavLink>

        <div className="new-post-input-ctr">
          <textarea
            ref={textareaRef}
            value={postContent}
            onChange={(e) => setPostContent(e.target.value)}
            placeholder="What's new?"
            className="new-post-input"
            style={{ resize: 'none', overflow: 'hidden' }}
          />
          <input
            ref={ImgInputRef}
            id="input"
            type="file"
            className="new-post-media-input"
            accept="image/*"
            onChange={handleImageSelection}
          />
          <label htmlFor="input" className="new-post-media-logo">
            <FaFileImage color="green" size={22} />
          </label>
        </div>

        {imgUrl && (
          <div className="new-post-img-ctr">
            <img src={imgUrl} className="post-img" alt="Post preview" />
            <button className="delete-img-icon" onClick={handleImageDeletion}>
              <MdOutlineDeleteForever size={27} color="#193cb8" />
            </button>
          </div>
        )}

        <div className="button-ctr">
          <button
            className="post-btn"
            onClick={() => {
              if (!selectedFile && !postContent.trim()) return;
              mutateNewPost({
                userId: user.userId,
                content: postContent.trim() || null,
                media: selectedFile
              });
            }}
          >
            Post
          </button>
        </div>
      </div>

      {/* Posts List */}
      {posts?.map(p => (
        <div key={p.postId} className="new-post-ctr">
          {/* Post Header */}
          <div className="post-header">
            <NavLink
              data-user-id={p.userId}
              to={p.userId === user.userId ? "/current-user" : "/user"}
              className="user-post-ctr"
              onClick={(e) => {
                if (e.currentTarget.dataset.userId !== user.userId) {
                  setTargetUserId(e.currentTarget.dataset.userId || "");
                }
              }}
            >
              <img
                src={p.profilePic ? p.profilePic : p.gender === "M" ? male : female}
                className="post-user-img"
                alt={`${p.firstName}'s profile`}
              />
              <p className="post-user-name">{p.firstName} {p.lastName}</p>
            </NavLink>
            <p className="post-time">{getTimeAgo(p.createdAt)}</p>
          </div>

          {/* Post Content */}
          {p.content && (
            <p className="post-content">
              {!expandedPosts[p.postId]
                ? `${p.content.slice(0, 101)}${p.content.length > 100 ? '...' : ''}`
                : p.content}
              {p.content.length >= 100 && (
                <span
                  className="show-more"
                  onClick={(e) => {
                    e.stopPropagation();
                    togglePostExpansion(p.postId);
                  }}
                >
                  {expandedPosts[p.postId] ? " show less" : " show more"}
                </span>
              )}
            </p>
          )}

          {/* Post Media */}
          {p.mediaUrl && (
            <img src={p.mediaUrl} className="post-media-img" alt="Post media" />
          )}

          {/* Likes & Comments Count */}
          <div className="likes-comments-count-ctr">
            <div
              className="likes-count-ctr"
              data-post-id={p.postId}
              onClick={(e) => {
                setIsOverlayVisible(true);
                setIsLikeVisible(true);
                setIsCommentVisible(false);
                setCurrentPostId(e.currentTarget.dataset.postId || "");
              }}
            >
              <button className="like-icon comment-icon">likes</button>
              <p className="likes-count">{p.likesCount}</p>
            </div>

            <div
              className="comments-count-ctr"
              data-post-id={p.postId}
              onClick={(e) => {
                setIsOverlayVisible(true);
                setIsCommentVisible(true);
                setIsLikeVisible(false);
                setCurrentPostId(e.currentTarget.dataset.postId || "");
              }}
            >
              <button className="comment-icon">comments</button>
              <p className="comments-count">{p.commentsCount}</p>
            </div>
          </div>

          {/* Like & Comment Actions */}
          <div className="like-comment-ctr">
            <button
              className="reactions-icons"
              onClick={() => {
                if (p.isLiked === 'yes') {
                  mutateDislike({ postId: p.postId, userId: user.userId });
                } else {
                  mutateNewLike({ postId: p.postId, userId: user.userId });
                  setTimeout(() => {
                    sendNotification(
                      JSON.stringify({
                        content: "has liked your post",
                        postId: p.postId,
                        userId: user.userId,
                        notificationTypeId: 1
                      })
                    );
                  }, 300);
                }
              }}
            >
              {p.isLiked === "yes" ? (
                <BiSolidLike size={22} color="blue" />
              ) : (
                <BiLike size={22} color="blue" />
              )}
            </button>

            <div
              className="reactions-icons"
              data-post-id={p.postId}
              onClick={(e) => {
                setIsOverlayVisible(true);
                setIsCommentVisible(true);
                setIsLikeVisible(false);
                setCurrentPostId(e.currentTarget.dataset.postId || "");
              }}
            >
              <FaRegComment size={22} />
            </div>
          </div>
        </div>
      ))}
    </div>
  );
}

export default Posts;