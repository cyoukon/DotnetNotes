using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace SignalRDemo.Hubs
{
    public class ChatHub : Hub
    {
        [HubMethodName(nameof(SendMessage))] // 可选attribute，用于自定义hub方法名
        public Task SendMessage(string user, string message)
        {
            return Clients.All.SendAsync("ReceiveMessage", user, message);
        }
    }
}
