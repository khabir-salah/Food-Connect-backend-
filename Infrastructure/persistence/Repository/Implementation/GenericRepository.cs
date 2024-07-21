using Application.Interfaces;
using Infrastructure.persistence.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.persistence.Repository.Implementation
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly FoodConnectDB _context;
        private readonly DbSet<T> Entity;
        public GenericRepository(FoodConnectDB context)
        {
            _context = context;
            Entity = context.Set<T>();
        }
        public void Delete(T entity)
        {
            _context.Remove(entity);
            _context.SaveChanges();
        }

        public async Task<T?> Get(Expression<Func<T, bool>> predicate)
        {
            return await  _context.Set<T>().FirstOrDefaultAsync(predicate);
            //return await Entity.FirstOrDefaultAsync(predicate);
        }


        public async Task<List<T>> GetAll()
        {
            return await _context.Set<T>().ToListAsync();
            //return  await Entity.ToListAsync();
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Update(T entity)
        {
            _context.Update(entity);
            _context.SaveChanges();
        }

        public void Add(T entity)
        {
            _context.Add(entity);
            _context.SaveChanges();
        }
    }
}
