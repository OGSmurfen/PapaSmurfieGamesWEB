using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using PapaSmurfie.Repository;

namespace PapaSmurfie.Web.Hubs
{
    [Authorize]
    public class SocialGroupsHub : Hub
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly SignInManager<IdentityUser> _signInManager;
        public SocialGroupsHub(IUnitOfWork unitOfWOrk, SignInManager<IdentityUser> signInManager)
        {
            _unitOfWork = unitOfWOrk;
            _signInManager = signInManager;
        }
        // Managing groups:
        public Dictionary<string, string> temporaryGroupsDict = new Dictionary<string, string>();
        public async Task CreateAndJoinGroup()
        {

            string groupName = "Lobby#" + Guid.NewGuid().ToString();

            Console.WriteLine(groupName);

            string myUsername = _signInManager.UserManager.GetUserName(Context.User);

            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);

            temporaryGroupsDict.Add(myUsername, groupName);

            await Clients.Group(groupName).SendAsync("GroupChat", $"{myUsername} has joined the group {groupName}.");

        }
        public async Task AddToGroup(string userToAdd)
        {
            string myUsername = _signInManager.UserManager.GetUserName(Context.User);

            string groupName = GetGroup(myUsername);

            if (groupName == null)
            {
                return;
            }

            await Groups.AddToGroupAsync(userToAdd, groupName);

            await Clients.Group(groupName).SendAsync("GroupChat", $"{myUsername} has joined the group {groupName}.");
        }
        public string GetGroup(string username)
        {
            if (temporaryGroupsDict.TryGetValue(username, out string groupName))
            {
                return groupName;
            }
            return null;
        }

        public async Task RemoveFromGroup(string groupName)
        {
            string myUsername = _signInManager.UserManager.GetUserName(Context.User);

            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);

            await Clients.Group(groupName).SendAsync("GroupChat", $"{myUsername} has left the group {groupName}.");
        }

        public async Task SendMessageToGroup(string groupName, string message)
        {
            string myUsername = _signInManager.UserManager.GetUserName(Context.User);

            await Clients.Group(groupName).SendAsync("GroupChat", $"{myUsername}: {message}");
        }
    }
}
