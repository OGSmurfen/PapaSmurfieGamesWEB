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
    public class FriendsRepository : RepositoryGeneric<FriendsList>, IFriendsRepository
    {
        public FriendsRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            
        }
    }
}
