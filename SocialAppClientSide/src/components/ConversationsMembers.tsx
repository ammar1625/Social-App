import user from "../images/user-male.png"

function ConversationsMembers()
{
    return <div className="conversations-members-ctr">
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
        </div>

        <div className="conversation-member">
            <img src={user} alt="user" className="conversation-member-img" />
            <p className="conversation-member-name">Ammar</p>
        </div>

        <div className="conversation-member">
            <img src={user} alt="user" className="conversation-member-img" />
            <p className="conversation-member-name">Ammar</p>
        </div>


      
    </div>
}

export default ConversationsMembers;