﻿using Microsoft.AspNetCore.SignalR;

namespace Application.Extensions
{
    public class DomainHub : Hub
    {
        public async Task SendNotification(string userId, string message)
        {
            await Clients.User(userId).SendAsync("ReceiveNotification", message);
        }
    }
}
