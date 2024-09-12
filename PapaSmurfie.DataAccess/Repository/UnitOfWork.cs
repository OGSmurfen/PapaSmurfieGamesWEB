using PapaSmurfie.DataAccess.Repository.IRepository;
using PapaSmurfie.Database;
using PapaSmurfie.Repository.IRepository;

namespace PapaSmurfie.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext applicationDbContext;
        public IGamesRepository GamesRepository { get; }
        public IOwnedGamesRepository OwnedGamesRepository { get; }
        public IFriendsRepository FriendsRepository { get; }
        public IUserStatusRepository UserStatusRepository { get; }
        public ILobbyRepository LobbyRepository { get; set; }

        public UnitOfWork(
            ApplicationDbContext applicationDbContext,
            IGamesRepository gamesRepository,
            IOwnedGamesRepository ownedGamesRepository,
            IFriendsRepository friendsRepository,
            IUserStatusRepository userStatusRepository,
            ILobbyRepository lobbyRepository

            )
        {
            this.applicationDbContext = applicationDbContext;
            this.GamesRepository = gamesRepository;
            this.OwnedGamesRepository = ownedGamesRepository;
            FriendsRepository = friendsRepository;
            UserStatusRepository = userStatusRepository;
            LobbyRepository = lobbyRepository;
        }

        public async Task Save()
        {
           await applicationDbContext.SaveChangesAsync();
        }
    }
}
