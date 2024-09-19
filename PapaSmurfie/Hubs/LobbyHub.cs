using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using PapaSmurfie.Models.Models;
using PapaSmurfie.Repository;

namespace PapaSmurfie.Web.Hubs
{
    [Authorize]
    public class LobbyHub : Hub
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly SignInManager<IdentityUser> _signInManager;
        public LobbyHub(IUnitOfWork unitOfWOrk, SignInManager<IdentityUser> signInManager)
        {
            _unitOfWork = unitOfWOrk;
            _signInManager = signInManager;
        }

        // Managing groups:
        public async Task CreateAndJoinLobby()
        {
            string myUsername = _signInManager.UserManager.GetUserName(Context.User);
            string userId = _signInManager.UserManager.GetUserId(Context.User);

            bool isUserInLobby = await _unitOfWork.LobbyRepository.IsUserInLobbyAsync(userId);
            if (isUserInLobby)
            {
                Console.WriteLine("LobbyHub.CreateAndJoinLobby: User already in lobby");
                return;
            }

            string lobbyName = "Lobby#" + Guid.NewGuid().ToString();
            string lobbyId = lobbyName.Substring(0, 14);

            Console.WriteLine(lobbyId);

            await Groups.AddToGroupAsync(Context.ConnectionId, lobbyId);

            
            await _unitOfWork.LobbyRepository.CreateLobbyUserRecordAsync(lobbyId, userId);

            await Clients.Group(lobbyId).SendAsync("GroupChat", $"{myUsername} has joined the group {lobbyId}.");


            // For displaying lobby id
            await Clients.Group(lobbyId).SendAsync("Joined", lobbyId);
        }
        public async Task JoinLobby(string lobbyId)
        {
            Console.WriteLine("JoinLobby Entered");

            string myUsername = _signInManager.UserManager.GetUserName(Context.User);
            string userId = _signInManager.UserManager.GetUserId(Context.User);

            

            if (string.IsNullOrEmpty(lobbyId))
            {
                Console.WriteLine("Nothing to join, lobbyId empty!");
                return;
            }

            await Groups.AddToGroupAsync(Context.ConnectionId, lobbyId);

            await Clients.Group(lobbyId).SendAsync("GroupChat", $"{myUsername} has joined the group {lobbyId}.");

            await _unitOfWork.LobbyRepository.CreateLobbyUserRecordAsync(lobbyId, userId);

            // For displaying lobby id
            await Clients.Group(lobbyId).SendAsync("Joined", lobbyId);
        }
        public async Task<string> GetGroup()
        {
            string userId = _signInManager.UserManager.GetUserId(Context.User);
            var lobbyRecord = await _unitOfWork.LobbyRepository.GetAsync(l => l.UserId == userId);

            return lobbyRecord.LobbyId;
        }


        // Lobby is more user friendly but the SignalR term is Group
        public async Task DisconnectFromLobby()
        {
            
            string userId = _signInManager.UserManager.GetUserId(Context.User);


            string lobbyId = await _unitOfWork.LobbyRepository.RemoveUserLobbyRecord(userId);
            if(string.IsNullOrEmpty(lobbyId))
            {
                Console.WriteLine("User was not in lobby. Disconnection abort");
                return;
            }

            Console.WriteLine("LobbyHub.CreateAndJoinLobby: Disconnecting from lobby");
            await RemoveFromGroup(lobbyId);

            // For displaying lobby id
            await Clients.Group(lobbyId).SendAsync("Disconnected", lobbyId);
        }

        public async Task RemoveFromGroup(string groupName)
        {
            string myUsername = _signInManager.UserManager.GetUserName(Context.User);

            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);

            await Clients.Group(groupName).SendAsync("GroupChat", $"{myUsername} has left the group {groupName}.");
        }

        public async Task SendMessageToGroup(string message)
        {
            string userId = _signInManager.UserManager.GetUserId(Context.User);
            var isUserInLobby = await _unitOfWork.LobbyRepository.IsUserInLobbyAsync(userId);
            if(!isUserInLobby)
            {
                return;
            }

            string myUsername = _signInManager.UserManager.GetUserName(Context.User);
            
            LobbyModel lobbyRecord = await _unitOfWork.LobbyRepository.GetAsync(l => l.UserId == userId);

            string groupName = lobbyRecord.LobbyId;

            await Clients.Group(groupName).SendAsync("GroupChat", $"{myUsername}: {message}");
        }
    }
}
