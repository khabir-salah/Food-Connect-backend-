using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Interfaces.IServices
{
    public interface ILogisticsService
    {
        Task<int> DisapprovedDonationCountAsync();
        Task<int> ApprovedDonationCountAsync();
        Task<int> PendingDonationCountAsync();
        Task<int> ReceivedDonationCountAsync();
        Task<int> AllPendingCountAsync();
        Task<int> AllApprovedCountAsync();
        Task<int> AllDisapprovedCountAsync();
        Task<int> AllExpiredCountAsync();
        Task<int> AllClaimedCountAsync();
        Task<int> AllReceivedCountAsync();
        Task<int> ExpiredDonationCountAsync();
    }
}
