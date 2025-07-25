import { FaFileImage } from "react-icons/fa6";
//import user from "../images/user-male.png"
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
function ConversationsRightSide()
{
    const {user} = userCurrentUserStore()
    const {userId} = useTargetUserIdStore();

    const [selectedFile ,setSelectedFile] = useState<File|null>(null);
    const [imgUrl ,setImgUrl] = useState("");
    const [shouldSend , setShouldSend] = useState(false);
    const [messageContent ,setMessageContent] = useState("");

    const ImgInputRef = useRef<HTMLInputElement>(null);
    const messageInputRef = useRef<HTMLInputElement>(null);

    const {conversationId} = useCurrentConversationIdStore()
    const {data:userData} = useGetUserById(userId);
    const {data:messagesData} = useGetMessagesList(conversationId);
    const {data:savedImageData, mutateAsync:mutateImageAsync} = useSaveImageMedia();

     function formatDateRelative(dateString: string): string {
        const inputDate = new Date(dateString);
        const now = new Date();
        
        // Normalize dates to compare only the date part (without time)
        const inputDateOnly = new Date(inputDate.getFullYear(), inputDate.getMonth(), inputDate.getDate());
        const todayOnly = new Date(now.getFullYear(), now.getMonth(), now.getDate());
        
        // Calculate difference in days
        const diffTime = inputDateOnly.getTime() - todayOnly.getTime();
        const diffDays = Math.floor(diffTime / (1000 * 60 * 60 * 24));
        
        // Format time as HH:MM
        const timeString = format(inputDate, 'HH:mm');
        
        // If it's today
        if (diffDays === 0) {
          return timeString;
        }
        
        // If it's within the last week (from yesterday to 6 days ago)
        // Note: We check absolute value since we're dealing with past dates
        if (diffDays < 0 && diffDays >= -6) {
          const dayName = format(inputDate, 'iii'); // Returns abbreviated day (e.g., "Sun")
          return `${dayName.toLowerCase()}, ${timeString}`;
        }
        
        // Otherwise, show full date and time
        const dateStringFormatted = format(inputDate, 'dd-MM-yy');
        return `${dateStringFormatted}, ${timeString}`;
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

          async function handleMessageSendAsync()
           {
            const messageValue = messageInputRef.current?.value.trim() ?? "";
            if(messageInputRef.current )
                {
                    if(!messageValue && !selectedFile)//handle empty message case
                    {
                        return;
                    }
                }

                if(selectedFile)//if the message constains an image then send it first and wait for the url from the response 
                {
                   await mutateImageAsync(selectedFile);
                   handleImageDeletion();
                   setShouldSend(true);
                }
                else //send the message directly without any media
                {
                    //console.log(messageValue);

                    
                }
                
           }

           useEffect(()=>{
                if(savedImageData)
                {
                    console.log("saved image Url ",savedImageData);
                }
           },[savedImageData]);

    return <div className="conversations-right-side-ctr">
        {selectedFile&&<button className="msg-media-close-btn" onClick={handleImageDeletion}><IoClose size={16} color="white"/></button>}
        {/*this is message media will show up everytime the user set an image to send with the message*/}
       {selectedFile && <div className="msg-media-ctr">       
            <img src={imgUrl} alt="message media" className="msg-media-img" />
        </div>}

        <div className="messages-part-header">
            <NavLink to="#" className="current-conversation-member">
                <img src={userData?.profilePic?userData.profilePic:userData?.gender==="M"? male:female} alt="user" className="current-conversation-member-img" />
                <p className="current-conversation-member-name">{userData?.firstName}</p>
            </NavLink>

            
        </div>

             <div className="messages-part-ctr">
                {
                    messagesData?.map(m=><div key={m.messageId} className={m.senderId===user.userId?"message-ctr":"message-ctr incoming-msg-ctr"} >
                       {m.content && <p className={m.senderId===user.userId?"message":"message incoming-message"}>{m.content}</p>} 
                        {m.messageMediaUrl &&<img className="msg-img" src={m.messageMediaUrl}/>}
                        <p className="message-time">{formatDateRelative(m.sentAt)}</p>
                    </div>)
                }
                  {/*   <div className="message-ctr incoming-msg-ctr">
                        <p className="message incoming-message">hello my name is Ammar and i am a full stack web developer my main programming language is c# and my fronyt end technoogy is react soon i will move into next js</p> 
                        <img className="msg-img" src={user}/>
                        <p className="message-time">2025-04-01,18:50</p>
                    </div>

                    <div className="message-ctr ">
                        <p className="message ">hello my name is Ammar and i am a full stack web developer my main programming language is c# and my fronyt end technoogy is react soon i will move into next js</p> 
                        <img src={user} className="msg-img"/>
                        <p className="message-time">2025-04-01,18:50</p>
                       
                    </div>

                    <div className="message-ctr ">
                        <p className="message ">hello my name is Ammar and i am a full stack web developer my main programming language is c# and my fronyt end technoogy is react soon i will move into next js</p> 
                        <p className="message-time">2025-04-01,18:50</p>
                    </div>

                    <div className="message-ctr ">
                        <p className="message ">hello my name is Ammar and i am a full stack web developer my main programming language is c# and my fronyt end technoogy is react soon i will move into next js</p> 
                        <p className="message-time">2025-04-01,18:50</p>
                    </div>

                    <div className="message-ctr ">
                        <p className="message ">hello my name is Ammar and i am a full stack web developer my main programming language is c# and my fronyt end technoogy is react soon i will move into next js</p> 
                        <p className="message-time">2025-04-01,18:50</p>
                    </div>

                    <div className="message-ctr ">
                        <p className="message ">hello my name is Ammar and i am a full stack web developer my main programming language is c# and my fronyt end technoogy is react soon i will move into next js</p> 
                        <p className="message-time">2025-04-01,18:50</p>
                    </div>

                    <div className="message-ctr ">
                        <p className="message ">hello my name is Ammar and i am a full stack web developer my main programming language is c# and my fronyt end technoogy is react soon i will move into next js</p> 
                        <p className="message-time">2025-04-01,18:50</p>
                    </div>

                    <div className="message-ctr ">
                        <p className="message ">hello my name is Ammar and i am a full stack web developer my main programming language is c# and my fronyt end technoogy is react soon i will move into next js</p> 
                        <p className="message-time">2025-04-01,18:50</p>
                    </div> */}

                    
            </div>

            <div className="send-message-ctr">
                <input ref={messageInputRef} type="text" className="send-input-field" />
                <input onChange={handleImageSelection} ref={ImgInputRef} id="msg-media-input" type="file" className="msg-media-input" accept="image/*" />
                <label htmlFor="msg-media-input" className="message-media-icon"><FaFileImage color="green" size={22}/></label>
                <button className="send-message-btn"
                    onClick={handleMessageSendAsync}
                ><BsSendFill size={25} color="blue"/></button>
            </div>
    </div>
}

export default ConversationsRightSide;