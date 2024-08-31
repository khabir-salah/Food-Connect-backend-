using Application.Features.Interfaces.IRepositries;
using Application.Features.Interfaces.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Queries.Get
{
    public class LogisticsService : ILogisticsService
    {
        private readonly IDonationRepository _donationRepo;
        private readonly IDonationService _donationService;
        public LogisticsService(IDonationRepository donationRepo, IDonationService donationService)
        {
            _donationRepo = donationRepo;
            _donationService = donationService;
        }

        public async Task<int> DisapprovedDonationCountAsync()
        {
            var user = await _donationService.ViewDisapprovedDonationByUser();
            return user.Data.Count;
        }

        public async Task<int> ExpiredDonationCountAsync()
        {
            var user = await _donationService.ExpiredDonationByUser();
            return user.Data.Count;
        }

        public async Task<int> ApprovedDonationCountAsync()
        {
            var user = await _donationService.ViewApprovedDonationByUser();
            return user.Data.Count;
        }

        public async Task<int> PendingDonationCountAsync()
        {
            var user = await _donationService.ViewPendingDonationByUser();
            return user.Data.Count;
        }

        public async Task<int> ReceivedDonationCountAsync()
        {
            var user = await _donationService.ViewReceivedDonationByUser();
            return user.Data.Count;
        }








        public async Task<int> AllPendingCountAsync()
        {
            var donation = await _donationRepo.GetAll();
            return donation.Where(d => d.Status == Domain.Enum.DonationStatus.pending).Count();
        }

        public async Task<int> AllApprovedCountAsync()
        {
            var donation = await _donationRepo.GetAll();
            return donation.Where(d => d.Status == Domain.Enum.DonationStatus.Approve).Count();
        }

        public async Task<int> AllDisapprovedCountAsync()
        {
            var donation = await _donationRepo.GetAll();
            return donation.Where(d => d.Status == Domain.Enum.DonationStatus.Disapprove).Count();
        }

        public async Task<int> AllClaimedCountAsync()
        {
            var donation = await _donationRepo.GetAll();
            return donation.Where(d => d.Status == Domain.Enum.DonationStatus.Claim).Count();
        }

        public async Task<int> AllExpiredCountAsync()
        {
            var donation = await _donationRepo.GetAll();
            return donation.Where(d => d.Status == Domain.Enum.DonationStatus.Expired).Count();
        }

        public async Task<int> AllReceivedCountAsync()
        {
            var donation = await _donationRepo.GetAll();
            return donation.Where(d => d.Status == Domain.Enum.DonationStatus.Received).Count();
        }
    }
}
