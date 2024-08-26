using PapaSmurfie.Repository.IRepository;

namespace PapaSmurfie.Repository
{
    public interface IUnitOfWork
    {
        IGamesRepository GamesRepository { get; }
        Task Save();
    }
}
