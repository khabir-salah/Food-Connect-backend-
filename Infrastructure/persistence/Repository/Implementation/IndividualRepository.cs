using Application.Features.Interfaces.IRepositries;
using Domain.Entities;
using Infrastructure.persistence.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.persistence.Repository.Implementation
{
    public class IndividualRepository : GenericRepository<Individual>, IIndividualRepository
    {
        private readonly FoodConnectDB _foodConnectDB;
        public IndividualRepository(FoodConnectDB context) : base(context)
        {
            _foodConnectDB = context;
        }

        public async Task<Individual?> GetIndividualUserAsync(Expression<Func<Individual, bool>> predicate)
        {
            return await _foodConnectDB.Individual.Include(r => r.User).FirstOrDefaultAsync(predicate);
        }
    }
}
