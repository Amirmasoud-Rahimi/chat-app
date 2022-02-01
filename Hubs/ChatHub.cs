using ChatApp.Entities;
using Microsoft.AspNet.SignalR.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace ChatApp.Hubs
{
    [HubName("ChatHun")]
    public class ChatHub:Hub
    {
        public async Task SendMessage(string user,Message msg)
        {
            await Clients.All.SendAsync("MessageReceived",user, msg);
        }

        public async Task OnConnect(string userName)
        {
            await Clients.All.SendAsync("UserConnected", userName);
        }

        public async Task OnDisConnect(string userName)
        {
            await Clients.All.SendAsync("UserDisConnected", userName);
        }
    }
}