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
    public class DonationRepository : GenericRepository<Donation>, IDonationRepository
    {
        private readonly FoodConnectDB _foodConnectDB;
        public DonationRepository(FoodConnectDB context) : base(context)
        {
            _foodConnectDB = context;
        }
        public async Task<Donation?> GetUserAsync(Expression<Func<Donation, bool>> predicate)
        {
            return await _foodConnectDB.Donation.Include(r => r.User).FirstOrDefaultAsync(predicate);
        }
    }
}
