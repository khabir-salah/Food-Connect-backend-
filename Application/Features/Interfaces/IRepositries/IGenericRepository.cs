using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Interfaces.IRepositries
{
    public interface IGenericRepository<T> where T : class
    {
        void Add(T entity);
        Task<List<T>> GetAll();
        Task<T> Get(Expression<Func<T, bool>> predicate);
        void Update(T entity);
        void Delete(T entity);
        void Save();
    }
}
