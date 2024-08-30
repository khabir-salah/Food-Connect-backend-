using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Interfaces.IRepositries
{
    public interface IIndividualRepository : IGenericRepository<Individual>
    {
        Task<Individual?> GetIndividualUserAsync(Expression<Func<Individual, bool>> predicate);
    }
}
