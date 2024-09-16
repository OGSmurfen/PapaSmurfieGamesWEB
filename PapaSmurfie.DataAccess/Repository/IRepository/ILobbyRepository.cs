using PapaSmurfie.Models.Models;
using PapaSmurfie.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PapaSmurfie.DataAccess.Repository.IRepository
{
    public interface ILobbyRepository : IRepositoryGeneric<LobbyModel>
    {
        public Task<bool> IsUserInLobbyAsync(string userId);
        public Task CreateLobbyUserRecordAsync(string lobbyId, string userId);
        /// <summary>
        /// Removes UserId-LobbyId record from Db and returns the LobbyId needed for actual disconnection in SignalR
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>string LobbyId</returns>
        public Task<string> RemoveUserLobbyRecord(string userId);
    }
}
