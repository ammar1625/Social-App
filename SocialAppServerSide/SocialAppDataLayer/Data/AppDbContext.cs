using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SocialAppDataLayer.Data.Config;
using SocialAppDataLayer.Models;

namespace SocialAppDataLayer.Data;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ChangePassWordToken> ChangePassWordTokens { get; set; }

    public virtual DbSet<Comment> Comments { get; set; }

    public virtual DbSet<Conversation> Conversations { get; set; }

    public virtual DbSet<DetailedConversationMember> ConversationsList { get; set; }

    public virtual DbSet<ConversationMember> ConversationMembers { get; set; }

    public virtual DbSet<DetailedFriendshipMember> FriendsList { get; set; }

    public virtual DbSet<FriendShipMember> FriendShipMembers { get; set; }

    public virtual DbSet<Friendship> Friendships { get; set; }

    public virtual DbSet<FriendshipExists> FriendshipExists { get; set; }

    public virtual DbSet<Invitation> Invitations { get; set; }

    public virtual DbSet<InvitationStatus> InvitationStatuses { get; set; }

    public virtual DbSet<Like> Likes { get; set; }

    public virtual DbSet<Message> Messages { get; set; }

    public virtual DbSet<Notification> Notifications { get; set; }

    public virtual DbSet<DetailedNotification> NotificationsList { get; set; }

    public virtual DbSet<NotificationType> NotificationTypes { get; set; }

    public virtual DbSet<Otp> Otps { get; set; }

    public virtual DbSet<Post> Posts { get; set; }

    public virtual DbSet<PostWithUserAndDetails> AllPosts { get; set; }
    public virtual DbSet<PostForCurrentUser> CurrentUserPosts { get; set; }
    public virtual DbSet<UnreadNotificationsCount> UnreadNotificationsCount { get; set; }

    public virtual DbSet<RefreshToken> RefreshTokens { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)

    {
        var config = new ConfigurationBuilder().AddJsonFile("appconfigs.json").Build();
        var ConnectionString = config.GetSection("ConnectionString").Value;
        optionsBuilder.UseSqlServer(ConnectionString);

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
       
        modelBuilder.ApplyConfiguration(new ChangePassWordTokenConfigs());
        modelBuilder.ApplyConfiguration(new CommentConfigs());
        modelBuilder.ApplyConfiguration(new ConversationConfigs());
        modelBuilder.ApplyConfiguration(new DetailedConversationMemberConfigs());
        modelBuilder.ApplyConfiguration(new ConversationMemberConfigs());
        modelBuilder.ApplyConfiguration(new DetailedFriendshipMemberConfigs());
        modelBuilder.ApplyConfiguration(new FriendShipMemberConfigs());
        modelBuilder.ApplyConfiguration(new FriendshipConfigs());
        modelBuilder.ApplyConfiguration(new FriendshipExistsConfigs());
        modelBuilder.ApplyConfiguration(new InvitationConfigs());
        modelBuilder.ApplyConfiguration(new InvitationStatusConfigs());
        modelBuilder.ApplyConfiguration(new LikeConfigs());
        modelBuilder.ApplyConfiguration(new MessageConfigs());
        modelBuilder.ApplyConfiguration(new NotificationConfigs());
        modelBuilder.ApplyConfiguration(new DetailedNotificationConfigs());
        modelBuilder.ApplyConfiguration(new UnreadNotificationsCountConfigs());
        modelBuilder.ApplyConfiguration(new NotificationTypeConfigs());
        modelBuilder.ApplyConfiguration(new OtpConfigs());
        modelBuilder.ApplyConfiguration(new PostConfigs());
        modelBuilder.ApplyConfiguration(new PostsWithUserAndDetaisConfigs());
        modelBuilder.ApplyConfiguration(new PostForCurrentUserConfigs());
        modelBuilder.ApplyConfiguration(new RefreshTokenConfigs());
        modelBuilder.ApplyConfiguration(new UserConfigs());

        //OnModelCreatingPartial(modelBuilder);
    }

    //partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
