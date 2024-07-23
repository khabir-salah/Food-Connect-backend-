using Application.Features.Interfaces.IRepositries;
using Domain.Entities;
using Infrastructure.persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.persistence.Repository.Implementation
{
    public class OrganisationRepository : GenericRepository<Organisation>, IOragnisationRepository
    {
        public OrganisationRepository(FoodConnectDB context) : base(context)
        {
        }
    }
}
