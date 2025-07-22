import { useEffect, useRef } from "react";
import { useGetConversationByMembersList } from "../hooks/useGetConversationMembersList";
import male from "../images/user.jpg";
import female from "../images/woman-icon.jpg";
import { useCurrentConversationIdStore } from "../stores/useCurrentConversationIdStore";
import { userCurrentUserStore } from "../stores/useCurrentUserStore";
import { useTargetUserIdStore } from "../stores/useTargetUserIdStrore";

function ConversationsMembers()
{
    const {user} = userCurrentUserStore();
    const { setUserId} = useTargetUserIdStore();
    const {setConversationId} = useCurrentConversationIdStore();
    const isFirstLoad = useRef(true);
    
    const {data:conversationMembersListData} = useGetConversationByMembersList(user.userId);

    //set the first conversation is the default selected conevrsation

    useEffect(() => {
      if (
        isFirstLoad.current &&
        conversationMembersListData &&
        conversationMembersListData.length > 0
      ) {
        setConversationId(conversationMembersListData[0].conversationId);
        setUserId(conversationMembersListData[0].userId);
        isFirstLoad.current = false; 
      }
    }, [conversationMembersListData]);


    //helper function to handle conversation member click
    const handleConversationMemberClick = (e:React.MouseEvent<HTMLDivElement> ) => {
        const target = e.target as Element;
        const memberDiv = target.closest(".conversation-member");
        if (!memberDiv) return;
      
        const data = memberDiv.getAttribute("data-infos");
        if (!data) return;
      
        try {
          const info = JSON.parse(data);
          //capture both user id and conversation id to get in to the conversation and display the user
          setUserId(info.userId);
          setConversationId(info.conversationId);
        } catch (err) {
          console.error("Invalid JSON in data-info:", err);
        }
      };
    return <div className="conversations-members-ctr">
       {/*  <div className="conversation-member">
            <img src={user} alt="user" className="conversation-member-img" />
            <p className="conversation-member-name">Ammar</p>
        </div> */}

        {conversationMembersListData?.map(c=><div data-infos = {JSON.stringify({
            userId:c.userId,
            conversationId:c.conversationId
        })} key={c.conversationMemberId} className="conversation-member" onClick={handleConversationMemberClick}>
            <img src={c.profilePic?c.profilePic:c.gender==="M"?male:female} alt="user" className="conversation-member-img" />
            <p className="conversation-member-name">{c.firstName}</p>
        </div>)}

        {/* <div className="conversation-member">
            <img src={user} alt="user" className="conversation-member-img" />
            <p className="conversation-member-name">Ammar</p>
        </div>

        <div className="conversation-member">
            <img src={user} alt="user" className="conversation-member-img" />
            <p className="conversation-member-name">Ammar</p>
        </div>

        <div className="conversation-member">
            <img src={user} alt="user" className="conversation-member-img" />
            <p className="conversation-member-name">Ammar</p>
        </div>

        <div className="conversation-member">
            <img src={user} alt="user" className="conversation-member-img" />
            <p className="conversation-member-name">Ammar</p>
        </div> */}


      
    </div>
}

export default ConversationsMembers;