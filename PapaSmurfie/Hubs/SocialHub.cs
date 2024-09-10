using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using PapaSmurfie.Models.Models;
using PapaSmurfie.Repository;
using System;

namespace PapaSmurfie.Web.Hubs
{
    [Authorize]
    public class SocialHub : Hub
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly SignInManager<IdentityUser> _signInManager;
        public SocialHub(IUnitOfWork unitOfWOrk, SignInManager<IdentityUser> signInManager)
        {
            _unitOfWork = unitOfWOrk;
            _signInManager = signInManager;
        }

        // Called when user connects with the hub
        public override async Task OnConnectedAsync()
        {
            // With this approach the CPU will be doing unnecessary work but I do not have a solution as of now
            // Except not send the message because the browser goes crazy
            // So whenever a user refreshes manually, they will see their friends' statuses
            // Database is being queried constantly however...
            // TODO: Fix

            string myId = _signInManager.UserManager.GetUserId(Context.User);

            await _unitOfWork.UserStatusRepository.CreateAsync(new UserStatusModel
            {
                UserId = myId
            });
            await _unitOfWork.Save();
            
            
            // Maybe will have to not send updates, because reload will happen too frequently, will see
            //var allFriendsOfThisUser = await _unitOfWork.FriendsRepository.GetThisUserFriendsIds(myId);
            //foreach (var friend in allFriendsOfThisUser)
            //{
            //    await Clients.User(friend).SendAsync("ReceiveFriendStatusUpdate");
            //}

            await base.OnConnectedAsync();

        }
        public override async Task OnDisconnectedAsync(Exception exception)
        {
            // With this approach the CPU will be doing unnecessary work but I do not have a solution as of now
            // Except not send the message because the browser goes crazy
            // So whenever a user refreshes manually, they will see their friends' statuses
            // Database is being queried constantly however...
            // TODO: Fix

            string myId = _signInManager.UserManager.GetUserId(Context.User);

            var myUserStatus = await _unitOfWork.UserStatusRepository.GetAsync(r => r.UserId == myId);
            _unitOfWork.UserStatusRepository.Delete(myUserStatus);

            await _unitOfWork.Save();

            // Maybe will have to not send updates, because reload will happen too frequently, will see
            //var allFriendsOfThisUser = await _unitOfWork.FriendsRepository.GetThisUserFriendsIds(myId);
            //foreach (var friend in allFriendsOfThisUser)
            //{
            //    await Clients.User(friend).SendAsync("ReceiveFriendStatusUpdate");
            //}


            await base.OnDisconnectedAsync(exception);
        }


        // Now signals are being sent from SocialController directly. Maybe reconfigure in the future
        //public async Task NotifyFriendRequestUpdate(string user)
        //{
        //    await Clients.User(user).SendAsync("ReceiveFriendRequestUpdate");
        //}
    }
}
