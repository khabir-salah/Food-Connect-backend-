using Application.Features.Command.Delete;
using Application.Features.DTOs;
using Application.Features.Interfaces.IServices;
using Application.Features.Queries.GeneralServices;
using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static Application.Features.DTOs.ViewDonationCommandModel;

namespace Api.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/Donation")]
    [ApiController]
    public class DonationController : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly IDonationService _donationService;
        private readonly ILogisticsService _logisticsService;
        private readonly IDonationFilter _donationFilter;
        private readonly IDonationValidation _donationValidation;

        public DonationController(IMediator mediator,  IDonationService donationService, ILogisticsService logisticsService, IDonationFilter donationFilter, IDonationValidation donationValidation)
        {
            this.mediator = mediator;
            this._donationService = donationService;
            _logisticsService = logisticsService;
            _donationFilter = donationFilter;
            _donationValidation = donationValidation;
        }

        [HttpPost("Create")]
        public async Task<IActionResult> CreateDonation(CreateDonationCommand request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var donation = await mediator.Send(request);
            if (!donation.IsSuccessfull)
            {
                return BadRequest(ModelState);
            }
            return Ok(donation.IsSuccessfull);
        }


        [HttpPost("Delete")]
        public async Task<IActionResult> DeleteDonation(DeleteDonation.DeleteDonationCommand request)
        {
            var delete = mediator.Send(request);
            if (!delete.IsCompletedSuccessfully)
            {
                return BadRequest(ModelState);
            }
            return Ok(delete);
        }

        [HttpGet("AllDonations")]
        public async Task<IActionResult> GetAllDonation([FromQuery] ViewDonationCommandModel.DonationCommand request)
        {
            var route = Request.Path.Value;
            var pagedResponse = await _donationService.PageResponse(route, request);
            return Ok(pagedResponse);
        }

        [HttpGet("Count")]
        public async Task<IActionResult> DonationCount()
        {
            var pendingCount = await _logisticsService.PendingDonationCountAsync();
            var approveCount = await _logisticsService.ApprovedDonationCountAsync();
            var receivedCount = await _logisticsService.ReceivedDonationCountAsync();
            var disapproveCount = await _logisticsService.DisapprovedDonationCountAsync();
            var expiredCount = await _logisticsService.ExpiredDonationCountAsync();

            var model = new DonationCountCommandModel
            {
                ApproveCount = approveCount,
                DisapproveCount = disapproveCount,
                PendingCount = pendingCount,
                ReceivedCount = receivedCount,
                ExpiredCount = expiredCount
            };
            return Ok(model);
        }

        [HttpGet("Pending")]
        public async Task<IActionResult> UserPendingDonation()
        {
            var donation = await _donationService.ViewPendingDonationByUser();
            if (donation.IsSuccessfull)
            {
                return Ok(donation.Data);
            }
            return Ok(null);
        }

        [HttpGet("Approved")]
        public async Task<IActionResult> UserApprovedDonation()
        {
            var donation = await _donationService.ViewApprovedDonationByUser();
            if (donation.IsSuccessfull)
            {
                return Ok(donation.Data);
            }
            return Ok(null);
        }

        [HttpGet("Expired")]
        public async Task<IActionResult> UserExpiredDonation()
        {
            var donation = await _donationService.ExpiredDonationByUser();
            if (donation.IsSuccessfull)
            {
                return Ok(donation.Data);
            }
            return Ok(null);
        }

        [HttpGet("Disapprove")]
        public async Task<IActionResult> UserDisapproedDonation()
        {
            var donation = await _donationService.ViewDisapprovedDonationByUser();
            if (donation.IsSuccessfull)
            {
                return Ok(donation.Data);
            }
            return Ok(null);
        }

        [HttpGet("Received")]
        public async Task<IActionResult> UserReceivedDonation()
        {
            var donation = await _donationService.ViewReceivedDonationByUser();
            if (donation.IsSuccessfull)
            {
                return Ok(donation.Data);
            }
            return Ok(null);
        }




        [HttpPut("Approve/{donationId}")]
        public async Task<IActionResult> ApproveDonationByManager(Guid donationId)
        {
            var approve = await _donationValidation.ApproveDonationByManager(donationId);
            if (approve.IsSuccessfull)
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpPut("Claim/{donationId}")]
        public async Task<IActionResult> ClaimDonation(Guid donationId)
        {
            var approve = await _donationValidation.ClaimDonation(donationId);
            if (approve.IsSuccessfull)
            {
                return Ok();
            }
            return BadRequest();
        }


        [HttpPost("Disapprove")]
        public async Task<IActionResult> DispproveDonationByManager(DisapproveDonationModel request)
        {
            var approve = await _donationValidation.DispproveDonation(request);
            if (approve.IsSuccessfull)
            {
                return Ok();
            }
            return BadRequest();
        }




        [HttpGet("claimedbyUser")]
        public async Task<IActionResult> ViewClaimedDonationByUser()
        {
            var claimed = await _donationService.ViewClaimedDonationByUser();
            if (!claimed.IsSuccessfull)
            {
                return Ok(claimed.Data);
            }
            return Ok(claimed.Data);
        }

        [HttpGet("claimedbyOtherUser")]
        public async Task<IActionResult> ViewClaimedDonationByOtherUser()
        {
            var claimed = await _donationService.ViewDonationsClaimedByOthers();
            if (!claimed.IsSuccessfull)
            {
                return Ok(claimed.Data);
            }
            return Ok(claimed.Data);
        }



        [HttpGet("Search")]
        public async Task<IActionResult> SearchDonation([FromQuery] DonationSearchCommand request)
        {
            var searchModel = await _donationFilter.SearchDonations(request);
            if (!searchModel.IsSuccessfull)
            {
                return Ok(searchModel);
            }
            return Ok(searchModel);
        }

        [HttpGet("AllSearch")]
        public async Task<IActionResult> SearchAllDonation([FromQuery] DonationSearchCommand request)
        {
            var searchModel = await _donationFilter.AllDonationSearch(request);
            if (!searchModel.IsSuccessfull)
            {
                return Ok(searchModel);
            }
            return Ok(searchModel);
        }

        [HttpGet("AllUserDonations")] 
        public async Task<IActionResult> GetAllDonationByUserAsync([FromQuery] PaginationFilter filter)
        {
            var route = Request.Path.Value;
            var pagedResponse = await _donationFilter.DonationPageResponse(route, filter);
            return Ok(pagedResponse);
        }

        [HttpGet("AllUserClaimableDonations")] 
        public async Task<IActionResult> GetAllClaimableDonationByUserAsync([FromQuery] PaginationFilter filter)
        {
            var route = Request.Path.Value;
            var pagedResponse = await _donationFilter.ClaimableDonationPageResponse(route, filter);
            return Ok(pagedResponse);
        }


        [HttpGet("GetAllDOnationCount")]
        public async Task<IActionResult> GetAllDonationCount()
        {
            var pendingCount = await _logisticsService.AllPendingCountAsync();    
            var approveCount = await _logisticsService.AllApprovedCountAsync();
            var disapproveCount = await _logisticsService.AllDisapprovedCountAsync();
            var expiredCount = await _logisticsService.AllExpiredCountAsync();
            var claimedCount = await _logisticsService.AllClaimedCountAsync();
            var receivedCount = await _logisticsService.AllReceivedCountAsync();

            var model = new DonationCountCommandModel
            {
                ApproveCount = approveCount,
                DisapproveCount = disapproveCount,
                PendingCount = pendingCount,
                ClaimedCount = claimedCount,
                ExpiredCount = expiredCount,
                ReceivedCount = receivedCount,
            };
            return Ok(model);
        }

        

    }
}
