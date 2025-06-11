using Bibliotheque.Models;

namespace Bibliotheque.Repositories
{
    public interface IRepository<T> where T : Media
    {
        Task<List<T>> GetAll();
        Task<T?> Get(int id);
        Task Add(T item);
        Task Update(T item);
        Task Delete(int id);
    }
}
