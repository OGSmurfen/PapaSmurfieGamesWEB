using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using PapaSmurfie.Models.Models;
using PapaSmurfie.Repository;
using System;

namespace PapaSmurfie.Web.Hubs
{
    [Authorize]
    public class StatusHub : Hub
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly SignInManager<IdentityUser> _signInManager;
        public StatusHub(IUnitOfWork unitOfWOrk, SignInManager<IdentityUser> signInManager)
        {
            _unitOfWork = unitOfWOrk;
            _signInManager = signInManager;
        }

        // Managing the Online-Offline status
        // Called when user connects with the hub
        public override async Task OnConnectedAsync()
        {
            string myId = _signInManager.UserManager.GetUserId(Context.User);

            await _unitOfWork.UserStatusRepository.CreateAsync(new UserStatusModel
            {
                UserId = myId
            });
            await _unitOfWork.Save();

            await base.OnConnectedAsync();

        }
        // Called when user disconnects from the hub
        public override async Task OnDisconnectedAsync(Exception exception)
        {
            string myId = _signInManager.UserManager.GetUserId(Context.User);

            var myUserStatus = await _unitOfWork.UserStatusRepository.GetAsync(r => r.UserId == myId);
            _unitOfWork.UserStatusRepository.Delete(myUserStatus);

            await _unitOfWork.Save();

            await base.OnDisconnectedAsync(exception);
        }


       

    }
}
