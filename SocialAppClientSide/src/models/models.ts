
export interface userToAdd
{
    firstName:string|undefined;

    lastName:string|undefined;
   
    userName:string|undefined;
   
    dateOfBirth:string|undefined;
   
    email:string|undefined;
   
    passWord:string|undefined;
   
    phone:string|undefined;
   
    isEmailVerified:boolean;
   
    isActive:boolean;

    gender:string;

}

export interface user
{
    firstName:string|undefined;

    lastName:string|undefined;
   
    userName:string|undefined;
   
    dateOfBirth:string|undefined;
   
    email:string|undefined;
   
    passWord:string|undefined;
   
    phone:string|undefined;
   
    isEmailVerified:boolean;
   
    isActive:boolean;

    gender:string;
    profilePic:string;

}


export interface userToFetch
{
    userId:string;
    firstName:string|undefined;

    lastName:string|undefined;
   
    userName:string|undefined;
   
    dateOfBirth:string|undefined;
   
    email:string|undefined;
   
    //passWord:string|undefined;
   
    phone:string|undefined;

    profilePic:string;
   
    isEmailVerified:boolean;
   
    isActive:boolean;
    
    token:string;

    refreshToken:string;

    gender:string;
}

export interface loginModel
{
    email:string|undefined;
    passWord:string|undefined;
}

export interface loginResponseModel
{
    isFound:boolean;
    isEmailVerified:boolean;
}

export interface changePassWordModel
{
    token:string|null;
    email:string|null;
    newPassword:string|undefined;
}

export interface postModel
{
    postId: string,
    content: string,
    mediaUrl: string,
    createdAt: string,
    userId: string,
    firstName: string,
    lastName: string,
    fullName:string,
    userName: string,
    dateOfBirth: string,
    email: string,
    passWord: string,
    phone: string,
    profilePic: string|null,
    isEmailVerified: boolean,
    isActive: boolean,
    gender:string,
    commentsCount: number,
    likesCount: number,
    isLiked: string
}

export interface postToAddModel
{
    content:string|null;
    media:File|null;
    userId:string;
}

export interface postsModel
{
    userId:string;
    currentUserId:string;
}



export interface tokensModel
{
    accessToken:string;
    refreshToken:string;
}

export interface refreshTokenModel
{
    userId:string;
    refreshToken:string;
}

export interface refreshTokenResponse
{
    refreshToken:string;
    jwt:string;
}

export interface notificationModel
{
    notificationId: string,
    content: string,
    postId: string,
    userId: string,
    notificationTypeId: number,
    notificationDate: string,
    isRead: boolean,
    firstName: string,
    lastName: string,
    profilePic: string,
    gender:string ,
    notificationRecieverId:string;
    /* user:userToFetch;
    post:postModel; */
}

export interface notificationToAddModel
{
        content:string;
        postId:string;
        userId :string;
        notificationTypeId:number;
       
}

export interface invitationModel
{
    invitationId: string,
    senderId: string,
    recieverId: string,
    invitationStatus: number,
    sentAt:string,
    reciever:user,
    sender:user
}

export interface changeinvitationsStatusMOdel
{
    invitationId:string;
    invitationStatus:number;
}

/* export interface invitationModel
{
    invitationId:string;
    senderId:string;
    recieverId:string;
    invitationStatus:number;
    sentAt:string;
    sender:user;
    reciever:user;
} */

export interface invitationToAddModel
{
    type:number;
    senderId:string;
    recieverId:string;
}

export interface friendshipMemberToAddModel
{
    friendshipId:string;
    userId:string;
}

export interface friendshipMemberToFetchModel
{
    friendShipMemberId: string;
    userId: string;
    firstName: string;
    lastName: string;
    userName: string;
    dateOfBirth: string;
    email: string;
    passWord:string;
    phone: string;
    profilePic: string|null;
    isEmailVerified: boolean;
    isActive: boolean;
    gender:string;
    friendShipId:string;
}

export interface friendshipModel
{
    friendShipId:string;
    createdAt:string;
}

export interface likeToFetchModel
{
    likeId: string;
    postId:  string;
    likedAt: string;
    userId:string;
    user:user
}

export interface likeToAddModel
{
    postId:string;
    userId:string;
}

export interface commentTofetchModel
{
    commentId: string;
    content: string;
    userId: string;
    postId: string;
    sentAt: string;
    user:user;
}

export interface commentToAddModel
{
    content:string;
    userId:string;
    postId:string;

}

export interface conversationToFetchModel
{
    currentUserId:string;
    targetUserId:string;
}

export interface conversationModel
{
    conversationId:string;
    createdAt:string;
}

export interface conversationMemberToAddModel
{
    conversationId:string;
    userId:string;
}

export interface conversationMemberToFetchModel
{
    
        conversationMemberId:string;
        conversationId: string;
        userId: string;
        user: user|null
      
}

export interface conversationMember
{
    conversationMemberId: string,
    userId: string,
    firstName: string,
    lastName: string,
    userName: string,
    dateOfBirth: string,
    email: string,
    passWord: string,
    phone:string,
    gender:string,
    profilePic: string|null,
    isEmailVerified: boolean,
    isActive: boolean,
    conversationId: string
  }

  export interface messageModel
  {
    messageId: string,
    senderId: string,
    conversationId: string,
    content: string|null,
    messageMediaUrl: string|null,
    sentAt: string,
    sender:user
  }

  export interface webSocketMessageModel
  {
    messageId: string,
    senderId: string,
    recieverId:string,
    conversationId: string,
    content: string|null,
    messageMediaUrl: string|null,
    sentAt: string,
  }

  export interface updateUserModel
  {
     userId: string|undefined,
    firstName: string|undefined,
    lastName: string|undefined,
    userName: string|undefined,
    dateOfBirth: string|undefined,
    email: string|undefined,
    phone: string|undefined,
    gender:string|undefined
  }

  export interface validatePassWordModel
  {
     userId:string;
     passWord:string;
  }

  export interface validatePassWordResponseModel
  {
     isValid:boolean;
  }

  export interface resetPassWordModel
  {
        newPassWord:string;
        email:string;

  }

  export interface changePassWordResponseModel
  {
        status:boolean;
        user:user;
  }

  export interface closeAccountModel
  {
        email:string|undefined;
        passWord:string;
  }

  export interface closeAccountResponseModel
  {
        isDeleted:boolean;
  }

  export interface logOutResponseModel
  {
        isLoggedOut:boolean;
  }
