using PapaSmurfie.Database;
using PapaSmurfie.Repository.IRepository;

namespace PapaSmurfie.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext applicationDbContext;
        public IGamesRepository GamesRepository { get; }

        public UnitOfWork(
            ApplicationDbContext applicationDbContext,
            IGamesRepository gamesRepository
            )
        {
            this.applicationDbContext = applicationDbContext;
            this.GamesRepository = gamesRepository;
        }

        public async Task Save()
        {
           await applicationDbContext.SaveChangesAsync();
        }
    }
}
