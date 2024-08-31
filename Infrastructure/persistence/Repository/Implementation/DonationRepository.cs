﻿using Application.Features.DTOs;
using Application.Features.Interfaces.IRepositries;
using Domain.Entities;
using Domain.Enum;
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
        public async Task<ICollection<Donation>> GetDonationByUserAsync(Expression<Func<Donation, bool>> predicate)
        {
            var donation =  _foodConnectDB.Donation.Include(r => r.User).Where(predicate);
            return await donation.ToListAsync();
        }

        public async Task<ICollection<Donation>> GetAllDonationByPage( PaginationFilter filter)
        {
            return await _foodConnectDB.Donation
                 .Include(r => r.User)
                 .ThenInclude(n => n.Role)
                .Skip((filter.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .ToListAsync();
        }

        public async Task<int> CountAsync( DonationStatus type)
        {
            var count = await _foodConnectDB.Donation.ToListAsync();
            var c =  count.Where(t => t.Status == type).Count();
            return c;
        }

        public async Task<Donation?> GetLastClaimByUser(Guid RecipientId)
        {
            return await _foodConnectDB.Donation
                .Where(r => r.Recipient == RecipientId)
                .OrderByDescending(r => r.LastClaimTime)
                .FirstOrDefaultAsync();
        }
    }
}
