using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace PapaSmurfie.Web.Hubs
{
    [Authorize]
    public class ChatHub : Hub
    {
        public async Task SendMessage(string user, string message)
        {
            var userName = Context.User.Identity.Name;
            await Clients.All.SendAsync("ReceiveMessage", userName, message);


            //await Clients.All.SendAsync("ReceiveMessage", user, message);
        }
    }
}
