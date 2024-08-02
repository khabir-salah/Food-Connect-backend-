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
    public class RoleRepository : GenericRepository<Role>, IRoleRepository
    {
        private readonly FoodConnectDB _foodConnectDB;
        public RoleRepository(FoodConnectDB context) : base(context)
        {
            _foodConnectDB = context;
        }

        public async Task<Role?> GetRoleByNameAsync(string name)
        {
            return await _foodConnectDB.Role.FirstOrDefaultAsync(x => x.Name == name);
        }


    }
}
