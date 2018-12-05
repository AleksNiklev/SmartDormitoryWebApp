using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SmartDormitary.Services.Hubs.Service
{
    public class HubService : IHubService
    {
        private readonly IHubContext<NotifyHub> hubContext;

        public HubService(IHubContext<NotifyHub> hubContext)
        {
            this.hubContext = hubContext;
        }

        public async Task Notify(string userId, string name, string value, string measurementType)
        {
            await hubContext.Clients.User(userId).SendAsync("Notify", name, value, measurementType);
        }
    }
}
