﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PapaSmurfie.Models.Models;
using PapaSmurfie.Models.Models.ViewModels;
using PapaSmurfie.Repository;
using PapaSmurfie.Utility;

namespace PapaSmurfie.Web.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class SocialController : Controller
    {

        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        public SocialController(
            ILogger<HomeController> logger,
            IUnitOfWork unitOfWork,
            SignInManager<IdentityUser> signInManager
            )
        {
            _signInManager = signInManager;
            _logger = logger;
            this._unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Index()
        {
            // Load all of User's friend requests into view
            var myUserId = _signInManager.UserManager.GetUserId(User);

            var outgoingPending = await _unitOfWork.FriendsRepository.GetAllAsync(
                                f => f.FriendshipSenderId == myUserId && f.Status == SD.FriendStatus_Pending);

            var incomingPending = await _unitOfWork.FriendsRepository.GetAllAsync(
                                f => f.FriendshipReceiverId == myUserId && f.Status == SD.FriendStatus_Pending);

            var acceptedFriends = await _unitOfWork.FriendsRepository.GetAllAsync(
                                f => (f.FriendshipSenderId == myUserId ||
                                     f.FriendshipReceiverId == myUserId) && 
                                     f.Status == SD.FriendStatus_Accepted);

            List<string> incomingPendingUsernames = new List<string>();
            foreach (var invite in incomingPending)
            {   
                // Because request is coming to User, we get the sender
                incomingPendingUsernames.Add(_signInManager.UserManager.Users.FirstOrDefault(
                                    u => u.Id == invite.FriendshipSenderId).UserName);
            }

            List<string> outgoingPendingUsernames = new List<string>();
            foreach (var invite in outgoingPending)
            {
                // Because request is outgoing from User, we get the receiver
                outgoingPendingUsernames.Add(_signInManager.UserManager.Users.FirstOrDefault(
                                    u => u.Id == invite.FriendshipReceiverId).UserName);
            }

            List<string> acceptedFriendsUsernames = new List<string>();
            foreach (var friends in acceptedFriends)
            {
                // No matter if sent or received, we want the other user's Id
                string usernameToAdd;
                var senderUsername = _signInManager.UserManager.Users.FirstOrDefault(
                                    u => u.Id == friends.FriendshipSenderId).UserName;
                usernameToAdd = senderUsername;
                if(senderUsername == User.Identity.Name)
                {
                    var receiverUsername = _signInManager.UserManager.Users.FirstOrDefault(
                                    u => u.Id == friends.FriendshipReceiverId).UserName;
                    usernameToAdd = receiverUsername;
                }
                acceptedFriendsUsernames.Add(usernameToAdd);
            }

            FriendRequestsVM allMyFriendRequests = new FriendRequestsVM
                    {
                        OutgoingPending = outgoingPendingUsernames,
                        IncomingPending = incomingPendingUsernames,
                        Accepted = acceptedFriendsUsernames
            };

            return View(allMyFriendRequests);
        }

        [HttpPost]
        public async Task<IActionResult> AddFriend(string username)
        {
            var friendToAdd = _signInManager.UserManager.Users.FirstOrDefault(u => u.UserName == username);

            if(friendToAdd == null)
            {
                TempData["StatusMessage"] = $"User with name: '{username}' not found";
                return RedirectToAction(nameof(Index));
            }
            if (_unitOfWork.FriendsRepository.GetAsync(
                f => f.FriendshipReceiverId == friendToAdd.Id).GetAwaiter().GetResult() != null ||
                _unitOfWork.FriendsRepository.GetAsync(
                f => f.FriendshipSenderId == friendToAdd.Id).GetAwaiter().GetResult() != null)
            {
                TempData["StatusMessage"] = $"Request already pending";
                return RedirectToAction(nameof(Index));
            }

            await _unitOfWork.FriendsRepository.CreateAsync(new FriendsList
            {
                FriendshipSenderId = _signInManager.UserManager.GetUserId(User),
                FriendshipReceiverId = friendToAdd.Id

            });

            await _unitOfWork.Save();

            TempData["StatusMessage"] = ("Friend Request Sent");
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> RemoveFriend(string friendUsername, string friendshipType)
        {
            var myUserId = _signInManager.UserManager.GetUserId(User);
            var friendId = _signInManager.UserManager.FindByNameAsync(friendUsername).GetAwaiter().GetResult().Id;

            if (friendshipType == SD.FriendshipType_Outgoing)
            {
                var friendshipToDelete = await _unitOfWork.FriendsRepository.GetAsync(
                    fr => fr.FriendshipReceiverId == friendId && fr.FriendshipSenderId == myUserId);

                _unitOfWork.FriendsRepository.Delete(friendshipToDelete);

                await _unitOfWork.Save();

                TempData["StatusMessage"] = ("Friend request removed!");
                return RedirectToAction(nameof(Index));
            }

            if(friendshipType == SD.FriendshipType_Incoming)
            {
                var friendshipToDelete = await _unitOfWork.FriendsRepository.GetAsync(
                    fr => fr.FriendshipReceiverId == myUserId && fr.FriendshipSenderId == friendId);

                _unitOfWork.FriendsRepository.Delete(friendshipToDelete);

                await _unitOfWork.Save();

                TempData["StatusMessage"] = ("Friend request removed!");
                return RedirectToAction(nameof(Index));
            }

            if(friendshipType == SD.FriendStatus_Accepted)
            {
                var friendshipToDelete = await _unitOfWork.FriendsRepository.GetAsync(fr => 
                        (fr.FriendshipReceiverId == myUserId && fr.FriendshipSenderId == friendId)||
                        (fr.FriendshipReceiverId == friendId && fr.FriendshipSenderId == myUserId));

                _unitOfWork.FriendsRepository.Delete(friendshipToDelete);

                await _unitOfWork.Save();

                TempData["StatusMessage"] = ("Friend removed!");
                return RedirectToAction(nameof(Index));
            }

            // If we got this far, something is wrong. Maybe friend changed his username at the exact same second.
            TempData["StatusMessage"] = ("Something went wrong");
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> AcceptFriend(string friendUsername)
        {
            var myUserId = _signInManager.UserManager.GetUserId(User);
            var friendId = _signInManager.UserManager.FindByNameAsync(friendUsername).GetAwaiter().GetResult().Id;

            var friendshipToAccept = await  _unitOfWork.FriendsRepository.GetAsync(
                    fr => fr.FriendshipReceiverId == myUserId && fr.FriendshipSenderId == friendId);

            friendshipToAccept.Status = SD.FriendStatus_Accepted;

            _unitOfWork.FriendsRepository.Update(friendshipToAccept);

            await _unitOfWork.Save();

            TempData["StatusMessage"] = ("Friend accepted!");
            return RedirectToAction(nameof(Index));
        }

    }
}
