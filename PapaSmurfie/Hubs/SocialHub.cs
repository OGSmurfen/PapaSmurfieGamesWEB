using Microsoft.AspNetCore.SignalR;

namespace PapaSmurfie.Web.Hubs
{
    public class SocialHub : Hub
    {
        // Now signals are being sent from SocialController directly. Maybe reconfigure in the future
        //public async Task NotifyFriendRequestUpdate(string user)
        //{
        //    await Clients.User(user).SendAsync("ReceiveFriendRequestUpdate");
        //}
    }
}
