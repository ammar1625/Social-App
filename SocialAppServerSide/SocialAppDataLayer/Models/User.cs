using System;
using System.Collections.Generic;

namespace SocialAppDataLayer.Models;

public partial class User
{
    public string UserId { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    //public  string  FullName 
    //{
    //    get
    //    { 
    //        return this.FirstName + " " + this.LastName;
    //    }
    //} 

    public string UserName { get; set; } = null!;

    public DateTime DateOfBirth { get; set; }

    public string Email { get; set; } = null!;

    public string PassWord { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public string? ProfilePic { get; set; }

    public bool IsEmailVerified { get; set; }

    public bool IsActive { get; set; }

    public char Gender { get; set; }

    public  ICollection<ChangePassWordToken> ChangePassWordTokens { get; set; } = new List<ChangePassWordToken>();

    public  ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public  ICollection<ConversationMember> ConversationMembers { get; set; } = new List<ConversationMember>();

    public  ICollection<FriendShipMember> FriendShipMembers { get; set; } = new List<FriendShipMember>();

    public  ICollection<Invitation> SentInvitations { get; set; } = new List<Invitation>();

    public  ICollection<Invitation> RecievedInvitations { get; set; } = new List<Invitation>();

    public  ICollection<Like> Likes { get; set; } = new List<Like>();

    public  ICollection<Message> Messages { get; set; } = new List<Message>();

    public  ICollection<Notification> Notifications { get; set; } = new List<Notification>();

    public  ICollection<Otp> Otps { get; set; } = new List<Otp>();

    public  ICollection<Post> Posts { get; set; } = new List<Post>();

    public  ICollection<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();
}
