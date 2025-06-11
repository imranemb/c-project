using Bibliotheque.Data;
using Bibliotheque.Models;
using Microsoft.EntityFrameworkCore;

namespace Bibliotheque.Repositories
{
    public class Repository<T> : IRepository<T> where T : Media
    {
        private readonly BibliothequeContext _context;
        private readonly DbSet<T> _dbSet;

        public Repository(BibliothequeContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public async Task<List<T>> GetAll() => await _dbSet.ToListAsync();

        public async Task<T?> Get(int id) => await _dbSet.FindAsync(id);

        public async Task Add(T item)
        {
            _dbSet.Add(item);
            await _context.SaveChangesAsync();
        }

        public async Task Update(T item)
        {
            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var item = await _dbSet.FindAsync(id);
            if (item != null)
            {
                _dbSet.Remove(item);
                await _context.SaveChangesAsync();
            }
        }
    }
}
