import ConversationsMembers from "./ConversationsMembers";
import ConversationsRightSide from "./ConversationsRightSide";

function Conversations()
{
    return <div className="conversations-ctr">
       <ConversationsMembers/>
       <ConversationsRightSide/>
    </div>


}

export default Conversations;