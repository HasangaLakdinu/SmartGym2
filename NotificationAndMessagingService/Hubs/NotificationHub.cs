using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using NotificationAndMessagingService.Context;
using NotificationAndMessagingService.Services;

namespace NotificationAndMessagingService
{
    [Authorize]
    public class NotificationHub:Hub
    {
        private readonly FitnessContext fitnessContext;
        private readonly NotificationContext _notificationContext;
        private readonly IUserService _userService;

        public NotificationHub(FitnessContext fitnessContext, IUserService userService, NotificationContext notificationContext)
        {
            fitnessContext = fitnessContext;
            _userService = userService;
            _notificationContext = notificationContext;
        }

        public override async Task OnConnectedAsync() {

            var userName = _userService.GetMyName();
            await Clients.All.SendAsync("ReceiveMessage", $"{userName} has joined");
            _notificationContext.UpdateOrCreateConnection(userName, Context.ConnectionId);
            //await Clients.All.SendAsync("ReceiveMessage", $"{Context.ConnectionId} has joined");

        }

        public async Task SendNotificationToAll(string message)
        {
            await Clients.All.SendAsync("ReceivedNotification", message);
        }

        public async Task SendNotificationToClient(string message, string username)
        {
            var hubConnections = _notificationContext.NotificationHubConnections.Where(con => con.Username == username).ToList();
            foreach (var hubConnection in hubConnections)
            {
                await Clients.Client(hubConnection.ConnectionId).SendAsync("ReceivedPersonalNotification", message, username);
            }
        }
    }
}
