using Application.Command;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IdentityController : ControllerBase
    {
        private readonly Mediator _mediator;

        public IdentityController(Mediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<IActionResult> CreateManager(CreateManager.ManagerRequestModel request)
        {
            if(!ModelState.IsValid) 
            {
                return BadRequest(request);
            }
            var response = await _mediator.Send(request);
            if(!response.IsSuccessfull)
            {
                return BadRequest(request);
            }
            return Ok(response.Data);
        }

        public async Task<IActionResult> CreateFamily(CreateFamily.FamilyRequestModel request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(request);
            }
            var response = await _mediator.Send(request);
            if (!response.IsSuccessfull)
            {
                return BadRequest(request);
            }
            return Ok(response.Data);
        }

        public async Task<IActionResult> CreateOrganization(CreateOrganization.OraganizationRequestModel request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(request);
            }
            var response = await _mediator.Send(request);
            if (!response.IsSuccessfull)
            {
                return BadRequest(request);
            }
            return Ok(response.Data);
        }

        public async Task<IActionResult> CreateRecipient(CreateRecipient.RecipientRequestModel request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(request);
            }
            var response = await _mediator.Send(request);
            if (!response.IsSuccessfull)
            {
                return BadRequest(request);
            }
            return Ok(response.Data);
        }
    }
}
