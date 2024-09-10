using PapaSmurfie.DataAccess.Repository.IRepository;
using PapaSmurfie.Database;
using PapaSmurfie.Models.Models;
using PapaSmurfie.Repository.RepositoryImpl;
using PapaSmurfie.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PapaSmurfie.DataAccess.Repository.RepositoryImpl
{
    public class FriendsRepository : RepositoryGeneric<FriendsListModel>, IFriendsRepository
    {
        public FriendsRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            
        }

        public async Task<IEnumerable<string>> GetThisUserFriendsIds(string userId)
        {

            var allIncomingAcceptedFriends = await this.GetAllAsync(fr => fr.FriendshipReceiverId == userId && fr.Status == SD.FriendStatus_Accepted);
            var allOutgoingAcceptedFriends = await this.GetAllAsync(fr => fr.FriendshipSenderId == userId && fr.Status == SD.FriendStatus_Accepted);

            var friendsToReturn = allIncomingAcceptedFriends.Select(fr => fr.FriendshipSenderId)
                            .Concat(allOutgoingAcceptedFriends.Select(fr => fr.FriendshipReceiverId));

            return friendsToReturn;
        }
    }
}
