using Application.Features.Command.Delete;
using Application.Features.DTOs;
using Application.Features.Interfaces.IServices;
using Application.Features.Queries.GeneralServices;
using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/Donation")]
    [ApiController]
    public class DonationController : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly IUriService _uriService;
        private readonly IDonationService _donationService;
        private readonly IMessageService _messageService;
        public DonationController(IMediator mediator, IUriService uriService, IDonationService donationService, IMessageService messageService)
        {
            this.mediator = mediator;
            this._uriService = uriService;
            this._donationService = donationService;
            _messageService = messageService;
        }

        [HttpPost("Create")]
        public async Task<IActionResult> CreateDonation(CreateDonationCommand request)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var donation = await mediator.Send(request);
            if(!donation.IsSuccessfull)
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
            var pendingCount = await _donationService.PendingDonationCountAsync();
            var approveCount = await _donationService.ApprovedDonationCountAsync();
            var receivedCount = await _donationService.ReceivedDonationCountAsync();
            var disapproveCount = await _donationService.DisapprovedDonationCountAsync();

            var model = new DonationCountCommandModel
            {
                ApproveCount = approveCount,
                DisapproveCount = disapproveCount,
                PendingCount = pendingCount,
                ReceivedCount = receivedCount
            };
            return Ok(model);
        }

        [HttpGet("Pending")]
        public async Task<IActionResult> UserPendingDonation()
        {
            var donation = await _donationService.ViewPendingDonationByUser();
            if(donation.IsSuccessfull)
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

        [HttpGet("{donationId}")]
        public async Task<IActionResult> GetDonationDetailsWithMessages(Guid donationId)
        {
            var donation = await _donationService.GetDonationByIdAsync(donationId);
            var messages = await _messageService.GetMessagesByDonationIdAsync(donationId);

            if (donation == null)
            {
                return NotFound();
            }

            var model = new DonationWithMessagesViewModel
            {
                Donation = donation,
                Messages = messages
            };

            return Ok(model);
        }


        [HttpPut("Approve/{donationId}")]
        public async Task<IActionResult> ApproveDonationByAdmin(Guid donationId)
        {
            var approve = await _donationService.ApproveDonationByAdmin(donationId);
            if(approve.IsSuccessfull)
            {
                return Ok();
            }
            return BadRequest();
        }


        [HttpPost("Disapprove")]
        public async Task<IActionResult> DispproveDonationByAdmin(DisapproveDonationModel request)
        {
            var approve = await _donationService.DispproveDonationByAdmin(request);
            if (approve.IsSuccessfull)
            {
                return Ok();
            }
            return BadRequest();
        }


        [HttpPost("{donationId}/mark-received")]
        public async Task<IActionResult> MarkDonationAsReceived(Guid donationId)
        {
            var received = await _donationService.DonationReceived(donationId);
            if (received.IsSuccessfull)
            {
                await _messageService.DeleteMessagesByDonationIdAsync(donationId);
                return Ok();
            }

            return BadRequest();
        }

        [HttpGet("claimed")]
        public async Task<IActionResult> ViewClaimedDonationByUser()
        {
            var claimed = await _donationService.ViewClaimedDonationByUser();
            if (claimed.IsSuccessfull)
            {
                return Ok(claimed);
            }
            return BadRequest();
        }


        [HttpPost("Search")]
        public async Task<IActionResult> SearchDonation(DonationSearchCommand request)
        {
            var searchModel = await _donationService.SearchDonations(request);
            if(searchModel.IsSuccessfull)
            {
                return Ok(searchModel.Data);
            }
            return BadRequest();
        }

        [HttpPost("All")]  
        public async Task<IActionResult> GetAllDonationByUserAsync()
        {
            var donationPage = await _donationService.GetAllDonationsForUser();
            if(donationPage.IsSuccessfull)
            {
                return Ok(donationPage.Data);
            }
            return BadRequest();
        }

    }
}
