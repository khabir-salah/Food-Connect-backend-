using Application.Features.Command.Update;
using Application.Features.DTOs;
using Application.Features.Interfaces.IServices;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    public class ProfileController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IViewProfile _viewProfile;
        private readonly ICurrentUser _currentUser;
        public ProfileController(IMediator mediator, IViewProfile viewProfile, ICurrentUser currentUser)
        {
            _mediator = mediator;
            _viewProfile = viewProfile;
            _currentUser = currentUser;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("UpdateProfile-OrganizationHead")]
        public async Task<IActionResult> OrganizationHeadProfileUpdate(OrganizationHeadModel request)
        {
            
            var response = await _mediator.Send(request);
            if (!response.IsSuccessfull)
            {
                return BadRequest(response.Message);
            }
            return View(response.Message);
        }

        [HttpPost("UpdateProfile-FamilyHead")]
        public async Task<IActionResult> FamilyHeadProfileUpdate(UpdateFamilyHeadModel request)
        {
            var response = await _mediator.Send(request);
            if (!response.IsSuccessfull)
            {
                return BadRequest(response.Message);
            }
            return View(response.Message);
        }

        [HttpPost("UpdateProfile-Individual")]
        public async Task<IActionResult> IndividualProfileUpdate(IndividualUpdateCommandModel request)
        {
           
            var response = await _mediator.Send(request);
            if (!response.IsSuccessfull)
            {
                return BadRequest(response.Message);
            }
            return View(response.Message);
        }

        [HttpPost("UpdateProfile-Manager")]
        public async Task<IActionResult> ManagerProfileUpdate(ManagerUpdateCommandModel request)
        {
          
            var response = await _mediator.Send(request);
            if (!response.IsSuccessfull)
            {
                return BadRequest(response.Message);
            }
            return View(response.Message);
        }

        [HttpGet("View-Profile")]
        public async Task<IActionResult> ViewUserProfile()
        {
            var user = await _currentUser.LoggedInUser();
            var userProfile = await _viewProfile.ViewProfileAsync(user.Id);
            if(!userProfile.IsSuccessfull)
            {
                return BadRequest(userProfile.Message);
            }
            return Ok(new
            {
                Role = user.Role.Name,
                ProfileData = userProfile.Data
            });
        }
    }
}
