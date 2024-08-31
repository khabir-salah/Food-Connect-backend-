using Application.Features.DTOs;
using Application.Features.Interfaces.IServices;
using Application.Features.Queries.Get;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/Profile")]
    [ApiController]
    public class ProfileController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IViewProfile _viewProfile;
        private readonly ICurrentUser _currentUser;
        private readonly IUserService _userService;

        public ProfileController(IMediator mediator, IViewProfile viewProfile, ICurrentUser currentUser, IUserService userService)
        {
            _mediator = mediator;
            _viewProfile = viewProfile;
            _currentUser = currentUser;
            _userService = userService;
        }
        //public IActionResult Index()
        //{
        //    return View();
        //}

        [HttpPost("UpdateProfile-OrganizationHead")]
        public async Task<IActionResult> OrganizationHeadProfileUpdate(OrganizationHeadModel request)
        {

            var response = await _mediator.Send(request);
            if (!response.IsSuccessfull)
            {
                return BadRequest(response.Message);
            }
            return Ok(response.IsSuccessfull);
        }

        [HttpPost("UpdateProfile-FamilyHead")]
        public async Task<IActionResult> FamilyHeadProfileUpdate(UpdateFamilyHeadModel request)
        {
            var response = await _mediator.Send(request);
            if (!response.IsSuccessfull)
            {
                return BadRequest(response.Message);
            }
            return Ok(response.IsSuccessfull);
        }

        [HttpPost("UpdateProfile-Individual")]
        public async Task<IActionResult> IndividualProfileUpdate(IndividualUpdateCommandModel request)
        {

            var response = await _mediator.Send(request);
            if (!response.IsSuccessfull)
            {
                return BadRequest(response.Message);
            }
            return Ok(response.IsSuccessfull);
        }

        [HttpPost("UpdateProfile-Manager")]
        public async Task<IActionResult> ManagerProfileUpdate(ManagerUpdateCommandModel request)
        {

            var response = await _mediator.Send(request);
            if (!response.IsSuccessfull)
            {
                return BadRequest(response.Message);
            }
            return Ok(response.Message);
        }

        [HttpGet("View-Profile")]
        public async Task<IActionResult> ViewUserProfile()
        {
            var user = await _currentUser.LoggedInUser();
            var userProfile = await _viewProfile.ViewProfileAsync(user.Id);
            if (!userProfile.IsSuccessfull)
            {
                return BadRequest(userProfile.Message);
            }
            return Ok(
                userProfile.Data);
        }

        [HttpGet("ViewAllUsers")]
        public async Task<IActionResult> ViewAllUsers([FromQuery] PaginationFilter request)
        {
            var route = Request.Path.Value;
            var users = await _userService.PageResponse(route, request);
            if(!users.IsSuccessfull)
            {
                return Ok(users.Message);
            }
            return Ok(users);
        }
        [HttpPut("Details/{donationId}")]
        public async Task<IActionResult> ApproveDonationByManager(Guid donationId)
        {
            var detail = await _userService.GetUserByIdAsync(donationId);
            if (detail.IsSuccessfull)
            {
                return Ok(detail.Data);
            }
            return BadRequest();
        }

        [HttpPut("Validate/{userId}")]
        public async Task<IActionResult> ValidateUserAsync(Guid userId)
        {
            var user = await _currentUser.ValidateUserAsync(userId);
            return Ok(user);
        }
    }
}
