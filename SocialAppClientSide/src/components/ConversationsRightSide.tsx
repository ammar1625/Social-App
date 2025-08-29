import { FaFileImage } from "react-icons/fa6";
import { BsSendFill } from "react-icons/bs";
import { NavLink } from "react-router-dom";
import { IoClose } from "react-icons/io5";
import { useGetUserById } from "../hooks/useGetUserById";
import { useTargetUserIdStore } from "../stores/useTargetUserIdStrore";
import male from "../images/user.jpg";
import female from "../images/woman-icon.jpg";
import { useCurrentConversationIdStore } from "../stores/useCurrentConversationIdStore";
import { useGetMessagesList } from "../hooks/useGetMessagesList";
import { userCurrentUserStore } from "../stores/useCurrentUserStore";
import { format } from 'date-fns';
import { useEffect, useRef, useState } from "react";
import { useSaveImageMedia } from "../hooks/useSaveMessageMedia";
import { useMessagesWebSocket } from "../hooks/useMessagesWebSocket";
import { useQueryClient } from "@tanstack/react-query";

function ConversationsRightSide() {
  const queryClient = useQueryClient();
  const { user } = userCurrentUserStore();
  const { userId } = useTargetUserIdStore();

  const [selectedFile, setSelectedFile] = useState<File | null>(null);
  const [imgUrl, setImgUrl] = useState("");
  const [shouldSend, setShouldSend] = useState(false);
  const [messageContent, setMessageContent] = useState("");

  const ImgInputRef = useRef<HTMLInputElement>(null);
  const messageTextareaRef = useRef<HTMLTextAreaElement>(null);

  const { conversationId } = useCurrentConversationIdStore();
  const { data: userData } = useGetUserById(userId);
  const { data: messagesData } = useGetMessagesList(conversationId);
  const { data: savedImageData, mutateAsync: mutateImageAsync } = useSaveImageMedia();

  const { sendMessage } = useMessagesWebSocket('ws://localhost:7890/messages', user.userId, conversationId, queryClient);

  // Format date (today → time, past → day + time)
  function formatDateRelative(dateString: string): string {
    const inputDate = new Date(dateString);
    const now = new Date();

    const inputDateOnly = new Date(inputDate.getFullYear(), inputDate.getMonth(), inputDate.getDate());
    const todayOnly = new Date(now.getFullYear(), now.getMonth(), now.getDate());

    const diffTime = inputDateOnly.getTime() - todayOnly.getTime();
    const diffDays = Math.floor(diffTime / (1000 * 60 * 60 * 24));
    const timeString = format(inputDate, 'HH:mm');

    if (diffDays === 0) {
      return timeString;
    }
    if (diffDays < 0 && diffDays >= -6) {
      const dayName = format(inputDate, 'iii');
      return `${dayName.toLowerCase()}, ${timeString}`;
    }
    const dateStringFormatted = format(inputDate, 'dd-MM-yy');
    return `${dateStringFormatted}, ${timeString}`;
  }

  // Handle image selection
  function handleImageSelection(e: React.ChangeEvent<HTMLInputElement>) {
    if (e.target.files && e.target.files.length > 0) {
      setSelectedFile(e.target.files[0]);
      setImgUrl(URL.createObjectURL(e.target.files[0]));
    }
  }

  // Remove image preview
  function handleImageDeletion() {
    setImgUrl("");
    setSelectedFile(null);
    if (ImgInputRef.current) ImgInputRef.current.value = "";
  }

  // Send message (text or with image)
  async function handleMessageSendAsync() {
    const trimmedContent = messageContent.trim();

    if (!trimmedContent && !selectedFile) {
      return;
    }

    if (selectedFile) {
      await mutateImageAsync(selectedFile);
      handleImageDeletion();
      setShouldSend(true);
    } else {
      sendMessage(JSON.stringify({
        senderId: user.userId,
        recieverId: userId,
        conversationId: conversationId,
        content: trimmedContent || null,
        messageMediaUrl: null,
      }));
      setMessageContent("");
    }
  }

  // Sync saved image with send
  useEffect(() => {
    if (savedImageData && shouldSend) {
      sendMessage(JSON.stringify({
        senderId: user.userId,
        recieverId: userId,
        conversationId: conversationId,
        content: messageContent.trim().length > 0 ? messageContent : null,
        messageMediaUrl: savedImageData,
      }));
      setMessageContent("");
      setShouldSend(false);
    }
  }, [savedImageData, shouldSend, messageContent, sendMessage, user.userId, userId, conversationId]);

  return (
    <div className="conversations-right-side-ctr">
      {/* Media Close Button */}
      {selectedFile && (
        <button
          className="msg-media-close-btn"
          onClick={handleImageDeletion}
          aria-label="Remove image"
        >
          <IoClose size={16} color="white" />
        </button>
      )}

      {/* Media Preview */}
      {selectedFile && (
        <div className="msg-media-ctr">
          <img src={imgUrl} alt="message media" className="msg-media-img" />
        </div>
      )}

      {/* Chat Header */}
      <div className="messages-part-header">
        <NavLink to="#" className="current-conversation-member">
          <img
            src={userData?.profilePic ? userData.profilePic : userData?.gender === "M" ? male : female}
            alt="user"
            className="current-conversation-member-img"
          />
          <p className="current-conversation-member-name">{userData?.firstName}</p>
        </NavLink>
      </div>

      {/* Messages List */}
      <div className="messages-part-ctr">
        {messagesData?.map((m) => (
          <div
            key={m.messageId}
            className={`message-ctr ${m.senderId === user.userId ? 'msg-ctr' : 'msg-ctr incoming-msg-ctr'}`}
          >
            {m.content && (
              <p className={`message ${m.senderId === user.userId ? 'message' : 'message incoming-message'}`}>
                {m.content}
              </p>
            )}
            {m.messageMediaUrl && <img src={m.messageMediaUrl} alt="sent media" className="msg-img" />}
            <p className="message-time">{formatDateRelative(m.sentAt)}</p>
          </div>
        ))}
      </div>

      {/* Message Input & Send */}
      <div className="send-message-ctr">
        <textarea
          ref={messageTextareaRef}
          value={messageContent}
          onChange={(e) => setMessageContent(e.target.value)}
          placeholder="Type a message..."
          className="send-input-field"
          // No auto-resize — fixed height with internal scroll
        />

        <input
          ref={ImgInputRef}
          id="msg-media-input"
          type="file"
          className="msg-media-input"
          accept="image/*"
          onChange={handleImageSelection}
        />

        <label htmlFor="msg-media-input" className="message-media-icon">
          <FaFileImage color="green" size={22} />
        </label>

        <button
          className="send-message-btn"
          onClick={handleMessageSendAsync}
          disabled={!messageContent.trim() && !selectedFile}
        >
          <BsSendFill size={25} color="blue" />
        </button>
      </div>
    </div>
  );
}

export default ConversationsRightSide;