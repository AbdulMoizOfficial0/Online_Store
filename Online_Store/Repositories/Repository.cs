using Microsoft.EntityFrameworkCore;
using Online_Store.Data;
using System.Linq.Expressions;

namespace Online_Store.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly ApplicationDbContext _context;
        protected readonly DbSet<T> _entities;

        public Repository(ApplicationDbContext context)
        {
            _context = context;
            _entities = context.Set<T>();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _entities.FindAsync();
        }
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _entities.ToListAsync();
        }

        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
        {
            return await _entities.Where(predicate).ToListAsync();
        }

        public async Task AddAsync(T entity)
        {
            await _entities.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public void Update(T entity)
        {
            _entities.Update(entity);
            _context.SaveChanges();
        }

        public void Remove(T entity)
        {
            _entities.Remove(entity);
            _context.SaveChanges();
        }

        public Task<IEnumerable<T>> FindAsync(Expression<Func<Task, bool>> predicate)
        {
            throw new NotImplementedException();
        }
    }
}
