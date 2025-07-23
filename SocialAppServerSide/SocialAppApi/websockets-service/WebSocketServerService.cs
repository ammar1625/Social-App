using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using WebSocketSharp.Server;

namespace SocialAppApi.websockets_service
{
    

    public class WebSocketServerService : BackgroundService
    {
        private WebSocketServer? _wss;

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _wss = new WebSocketServer("ws://localhost:7890"); // You can change the port

            _wss.AddWebSocketService<NotificationsBehavior>("/notifications"); // notifications behaviour endpoint
            _wss.AddWebSocketService<InvitationsBehaviour>("/invitations"); // invitations behaviour endpoint
            _wss.AddWebSocketService<MessagesBehaviour>("/messages"); // messages behaviour endpoint

            _wss.Start();

            // Stop gracefully
            stoppingToken.Register(() =>
            {
                _wss?.Stop();
            });

            return Task.CompletedTask;
        }
    }

}
