using Application.Features.Command.Delete;
using Application.Features.DTOs;
using Application.Features.Interfaces.IServices;
using Application.Features.Queries.GeneralServices;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/Donation")]
    [ApiController]
    public class DonationController : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly IUriService _uriService;
        private readonly IDonationService _donationService;
        public DonationController(IMediator mediator, IUriService uriService, IDonationService donationService)
        {
            this.mediator = mediator;
            this._uriService = uriService;
            this._donationService = donationService;
        }

        [HttpPost("Create-Donation")]
        public async Task<IActionResult> CreateDonation(CreateDonationCommandModel.CreateDonationCommand request)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var donation = mediator.Send(request);
            if(!donation.IsCompletedSuccessfully)
            {
                return BadRequest(ModelState);
            }
            return Ok(donation);
        }


        [HttpPost("Delete-Donation")]
        public async Task<IActionResult> DeleteDonation(DeleteDonation.DeleteDonationCommand request)
        {
            var delete = mediator.Send(request);
            if (!delete.IsCompletedSuccessfully)
            {
                return BadRequest(ModelState);
            }
            return Ok(delete);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllDonation([FromQuery] ViewDonationCommandModel.DonationCommand request)
        {
            var route = Request.Path.Value;
           var pagedResponse = await _donationService.PageResponse(route, request);
            return Ok(pagedResponse);
        }

    }
}
