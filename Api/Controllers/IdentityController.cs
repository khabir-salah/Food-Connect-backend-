using Application.Command;
using Application.Interfaces;
using Application.Queries;
using Asp.Versioning;
using Domain.Entities;
using Infrastructure.persistence.Repository.Implementation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using static Application.Queries.ResetPasswordService;
using static Application.Queries.UserLogin;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Api.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/Identity")]
    [ApiController]
    public class IdentityController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly UserManager<User> _userManager;
        private readonly Authentication _authentication;
        private readonly EmailService _emailService;
        private readonly ResetPasswordService _resetPasswordService;
        private readonly IUserRepository _userRepo;

        public IdentityController(IMediator mediator, Authentication authentication, UserManager<User> userManager, EmailService emailService, ResetPasswordService resetPasswordService, IUserRepository userRepo)
        {
            _mediator = mediator;
            _authentication = authentication;
            _userManager = userManager;
            _emailService = emailService;
            _resetPasswordService = resetPasswordService;
            _userRepo = userRepo;
        }


        [HttpPost("RegisterManager")]
        public async Task<IActionResult> CreateManager(CreateManager.ManagerRequestModel request)
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
            var user = await _userManager.FindByIdAsync(response.Data.UserId.ToString());
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var callbackUrl = Url.Action("ConfirmEmail", "Identity", new { userId = user.Id, token }, protocol: HttpContext.Request.Scheme);
            await _emailService.SendEmailConfirmationAsync(user.Email, callbackUrl);

            return Ok(new { Message = "Registration successful. Please check your email to confirm your account." });
        }


        [HttpPost("RegisterFamily")]
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
            var user = await _userManager.FindByIdAsync(response.Data.UserId.ToString());
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var callbackUrl = Url.Action("ConfirmEmail", "Identity", new { userId = user.Id, token }, protocol: HttpContext.Request.Scheme);
            await _emailService.SendEmailConfirmationAsync (user.Email, callbackUrl);
            return Ok(new { Message = "Registration successful. Please check your email to confirm your account." });
        }



        [HttpPost("RegisterOraganization")]
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

            var user = await _userManager.FindByIdAsync(response.Data.UserId.ToString());
            var token = await _userManager.GenerateEmailConfirmationTokenAsync (user);
            var callbackUrl = Url.Action("ConfirmEmail", "Identity", new { userId = user.Id, token }, protocol:
                HttpContext.Request.Scheme);
            await _emailService.SendEmailConfirmationAsync(user.Email, callbackUrl);
            return Ok(new { Message = "Registration successful. Please check your email to confirm your account." });
        }


        [HttpPost("RegisterRecipient")]
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
            var user = await _userManager.FindByIdAsync(response.Data.UserId.ToString());
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var callbackUrl = Url.Action("ConfirmEmail", "Identity", new { userId = user.Id, token }, protocol : HttpContext.Request.Scheme);
            await _emailService.SendEmailConfirmationAsync (user.Email, callbackUrl);
            return Ok(new { Message = "Registration successful. Please check your email to confirm your account." });
        }


        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginRequestModel request)
        {
            var response = await _mediator.Send(request);
            if (!response.IsSuccessfull)
            {
                return Unauthorized();
            }

            var token = _authentication.GenerateJWTAuthetication(response.Data.Email, response.Data.RoleId, response.Data.UserId);
            return Ok(new { Token = token });
        }

        [HttpGet("confirm-email")]
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            if (userId == null || token == null)
            {
                return BadRequest("Invalid email confirmation request.");
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{userId}'.");
            }

            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (result.Succeeded)
            {
                return Ok("Email confirmed successfully.");
            }
            return BadRequest("Invalid email confirmation request.");
        }



        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordRequest model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return Ok();
            }

            var token = _resetPasswordService.GeneratePasswordResetToken(user);
            await _userRepo.SavePasswordResetTokenAsync(user, token);

            var callbackUrl = Url.Action("ResetPassword", "Account", new { token }, Request.Scheme);
            await _emailService.SendPasswordResetEmailAsync(model.Email, callbackUrl);

            return Ok();
        }


        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword(ResetPasswordRequest model)
        {
            var user = await _userRepo.Get(t => t.PasswordResetToken == model.Token);
            if (user == null || user.PasswordExpireTime < DateTime.UtcNow)
            {
                return BadRequest("Invalid or expired token.");
            }

            user.Password = BCrypt.Net.BCrypt.HashPassword( model.Password, user.PasswordResetToken);
            user.PasswordResetToken = null; // Invalidate token
            user.PasswordExpireTime = null;
            _userRepo.Save();

            return Ok();
        }


    }
}
