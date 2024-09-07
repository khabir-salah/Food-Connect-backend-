using Application.Features.DTOs;
using Domain.Entities;
using Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Interfaces.IRepositries
{
    public interface IDonationRepository : IGenericRepository<Donation>
    {
        Task<ICollection<Donation>> GetDonationByUserAsync(Expression<Func<Donation, bool>> predicate);
        Task<ICollection<Donation>> GetAllDonationByPage();
        Task<int> CountAsync(DonationStatus type);
        Task<Donation?> GetLastClaimByUser(Guid RecipientId);
    }
}
