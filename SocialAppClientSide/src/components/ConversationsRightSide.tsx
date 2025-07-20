import { FaFileImage } from "react-icons/fa6";
import user from "../images/user-male.png"
import { BsSendFill } from "react-icons/bs";
import { NavLink } from "react-router-dom";
import { IoClose } from "react-icons/io5";
function ConversationsRightSide()
{
    return <div className="conversations-right-side-ctr">
        <button className="msg-media-close-btn"><IoClose size={16} color="white"/></button>
        {/*this is message media will show up everytime the user set an image to send with the message*/}
        <div className="msg-media-ctr">       
            <img src={user} alt="message media" className="msg-media-img" />
        </div>

        <div className="messages-part-header">
            <NavLink to="#" className="current-conversation-member">
                <img src={user} alt="user" className="current-conversation-member-img" />
                <p className="current-conversation-member-name">Ammar</p>
            </NavLink>

            
        </div>

             <div className="messages-part-ctr">
                    <div className="message-ctr incoming-msg-ctr">
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
                    </div>

                    
            </div>

            <div className="send-message-ctr">
                <input type="text" className="send-input-field" />
                <input id="msg-media-input" type="file" className="msg-media-input" accept="image/*" />
                <label htmlFor="msg-media-input" className="message-media-icon"><FaFileImage color="green" size={22}/></label>
                <button className="send-message-btn"><BsSendFill size={25} color="blue"/></button>
            </div>
    </div>
}

export default ConversationsRightSide;