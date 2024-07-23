using Application.Features.Command.Delete;
using Application.Features.DTOs;
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
        public DonationController(IMediator mediator)
        {
            this.mediator = mediator;
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

    }
}
