using Microsoft.EntityFrameworkCore;
using PapaSmurfie.DataAccess.Repository.IRepository;
using PapaSmurfie.Database;
using PapaSmurfie.Models.Models;
using PapaSmurfie.Repository.RepositoryImpl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PapaSmurfie.DataAccess.Repository.RepositoryImpl
{
    public class LobbyRepository : RepositoryGeneric<LobbyModel>, ILobbyRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<LobbyModel> _lobbyDbSet;
        public LobbyRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;  
            _lobbyDbSet = context.Set<LobbyModel>();
        }

        public async Task CreateLobbyUserRecordAsync(string lobbyId, string userId)
        {
            LobbyModel lobbyRecord = new LobbyModel
            {
                UserId = userId,
                LobbyId = lobbyId
            };

            await _context.Lobbies.AddAsync(lobbyRecord);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> IsUserInLobbyAsync(string userId)
        {
            return await _lobbyDbSet.AnyAsync(l => l.UserId == userId);
        }
        
        
        public async Task<string> RemoveUserLobbyRecord(string userId)
        {
            var lobby = await _lobbyDbSet.FirstOrDefaultAsync(l => l.UserId == userId);

            if (lobby != null)
            {
                _lobbyDbSet.Remove(lobby);
                await _context.SaveChangesAsync();
                return lobby.LobbyId;
            }
            else
            {
                return "";
            }
            

            
        }


    }
}
