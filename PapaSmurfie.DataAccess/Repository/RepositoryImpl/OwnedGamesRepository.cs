using PapaSmurfie.DataAccess.Repository.IRepository;
using PapaSmurfie.Database;
using PapaSmurfie.Models;
using PapaSmurfie.Repository.IRepository;
using PapaSmurfie.Repository.RepositoryImpl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PapaSmurfie.DataAccess.Repository.RepositoryImpl
{
    public class OwnedGamesRepository : RepositoryGeneric<OwnedGameModel>, IOwnedGamesRepository
    {
        private readonly ApplicationDbContext applicationDbContext;
        public OwnedGamesRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext) 
        {
            
        }
    }
}
