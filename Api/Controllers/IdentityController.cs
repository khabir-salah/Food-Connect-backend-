﻿using Application.Features.DTOs;
using Application.Features.Interfaces.IServices;
using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;


namespace Api.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/Identity")]
    [ApiController]
    public class IdentityController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IAuthentication _authentication;
        private readonly IEmailService _emailService;
        private readonly ICurrentUser _user;
        private readonly ITokenService _tokenService;

        public IdentityController(IMediator mediator, IAuthentication authentication, IEmailService emailService, ICurrentUser user, ITokenService tokenService)
        {
            _mediator = mediator;
            _authentication = authentication;
            _emailService = emailService;
            _user = user;
            _tokenService = tokenService;
        }


        [HttpPost("Register-Manager")]
        public async Task<IActionResult> CreateManager(CreateManagerCommandModel.CreateManagerCommand request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(request);
            }
            var response = await _mediator.Send(request);
            if (!response.IsSuccessfull)
            {
                return BadRequest(response.Message);
            }
            var user = await _user.GetUserAsync(response.Data.UserId);
            var token = await _tokenService.GenerateEmailConfirmationToken(response.Data.UserId);
            var callbackUrl = Url.Action("ConfirmEmail", "Identity", new { userId = user.Id, token }, protocol: HttpContext.Request.Scheme);
            await _emailService.SendEmailConfirmationAsync(user.Email, callbackUrl);

            return Ok(new { Message = "Registration successful. Please check your email to confirm your account." });
        }


        [HttpPost("Register-Family")]
        public async Task<IActionResult> CreateFamily(CreateFamilyHeadCommandModel.CreateFamilyHeadCommand request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(request);
            }
            var response = await _mediator.Send(request);
            if (!response.IsSuccessfull)
            {
                return BadRequest(response.Message);
            }
            var user = await _user.GetUserAsync(response.Data.UserId);
            var token = await _tokenService.GenerateEmailConfirmationToken(response.Data.UserId);
            var callbackUrl = Url.Action("ConfirmEmail", "Identity", new { userId = user.Id, token }, protocol: HttpContext.Request.Scheme);
            await _emailService.SendEmailConfirmationAsync(user.Email, callbackUrl);

            return Ok(new { Message = "Registration successful. Please check your email to confirm your account." });
        }



        [HttpPost("Register-Organization")]
        public async Task<IActionResult> CreateOrganization([FromBody] CreateOrganizationCommandModel.CreateOrganizationCommand request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(request);
            }
            var response = await _mediator.Send(request);
            if (!response.IsSuccessfull)
            {
                return BadRequest(response.Message);
            }

            var user = await _user.GetUserAsync(response.Data.UserId);
            var token = await _tokenService.GenerateEmailConfirmationToken(response.Data.UserId);
            var callbackUrl = Url.Action("ConfirmEmail", "Identity", new { userId = user.Id, token }, protocol: HttpContext.Request.Scheme);
            await _emailService.SendEmailConfirmationAsync(user.Email, callbackUrl);

            return Ok(new { Message = "Registration successful. Please check your email to confirm your account." });
        }


        [HttpPost("Register-Individual")]
        public async Task<IActionResult> CreateRecipient(CreateIndividualCommandModel.CreateIndividualCommand request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("All field must be completed");
            }
            var response = await _mediator.Send(request);
            if (!response.IsSuccessfull)
            {
                return BadRequest(response.Message);
            }
            var user = await _user.GetUserAsync(response.Data.UserId);
            var token =  await _tokenService.GenerateEmailConfirmationToken(response.Data.UserId);
            var callbackUrl = Url.Action("ConfirmEmail", "Identity", new { userId = user.Id, token }, protocol: HttpContext.Request.Scheme);
            await _emailService.SendEmailConfirmationAsync(user.Email, callbackUrl);

            return Ok(new { Message = "Registration successful. Please check your email to confirm your account." });
        }


        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginCommandModel.LoginCommand request)
        {
            var response = await _mediator.Send(request);
            if (!response.IsSuccessfull)
            {
                return Unauthorized(response.Message);
            }

            var getUser = await _user.GetUserAsync(response.Data.UserId);
            var token = _authentication.GenerateToken(getUser);
            if(getUser.IsEmailConfirmed)
            {
                return Ok(new { response.Message, Token = token});
            }
            return Unauthorized(response.Message);
        }

        [HttpGet("confirm-email")]
        public async Task<IActionResult> ConfirmEmail(Guid userId, string token)
        {
            if (userId == null || token == null)
            {
                return BadRequest("Invalid email confirmation request.");
            }

            var user = await _user.GetUserAsync(userId);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{userId}'.");
            }

            var result = await _tokenService.ValidateEmailTokenAsync(user.Id, token);
            if (!result)
            {
                return BadRequest("Invalid email confirmation request.");
            }
            return Ok("Email confirmed successfully.");
        }



        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword(PasswordModel.ForgotPasswordRequest model)
        {
            var user = await _user.GetUserEmailAsync(model.Email);
            if (user == null)
            {
                return BadRequest();
            }

            var token = _tokenService.GeneratePasswordResetToken(user);
            await _user.SavePasswordResetTokenAsync(user, token);

            var callbackUrl = Url.Action("reset-password", "Identity", new { token }, Request.Scheme);
            await _emailService.SendPasswordResetEmailAsync(model.Email, callbackUrl);

            return Ok();
        }
        

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword(PasswordModel.ResetPasswordRequest model)
        {
            var user = await _user.GetUserTokenAsync(model.Token);
            if (user == null || user.PasswordExpireTime < DateTime.UtcNow)
            {
                return BadRequest("Invalid or expired token.");
            }

            user.Password = BCrypt.Net.BCrypt.HashPassword( model.Password, user.PasswordResetToken);
            user.PasswordResetToken = null; 
            user.PasswordExpireTime = null;
            _user.SaveUserAsync();

            return Ok();
        }


    }
}
