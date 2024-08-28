using PapaSmurfie.Database;
using PapaSmurfie.Models;
using PapaSmurfie.Repository.IRepository;

namespace PapaSmurfie.Repository.RepositoryImpl
{
    public class GamesRepository : RepositoryGeneric<GameModel>, IGamesRepository
    {
       private readonly ApplicationDbContext applicationDbContext;
        public GamesRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext) { }
    }
}
