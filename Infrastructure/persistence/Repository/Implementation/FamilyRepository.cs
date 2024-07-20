using Application.Interfaces;
using Domain.Entities;
using Infrastructure.persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.persistence.Repository.Implementation
{
    public class FamilyRepository : GenericRepository<Family>, IFamilyRepository
    {
        public FamilyRepository(FoodConnectDB context) : base(context)
        {
        }
    }
}
