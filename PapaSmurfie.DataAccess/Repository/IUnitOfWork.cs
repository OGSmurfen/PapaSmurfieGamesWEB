using PapaSmurfie.DataAccess.Repository.IRepository;
using PapaSmurfie.Repository.IRepository;

namespace PapaSmurfie.Repository
{
    public interface IUnitOfWork
    {
        IGamesRepository GamesRepository { get; }
        IOwnedGamesRepository OwnedGamesRepository { get; }

        Task Save();
    }
}
