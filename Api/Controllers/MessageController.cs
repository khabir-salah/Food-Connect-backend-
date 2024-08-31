using Application.Features.DTOs;
using Application.Features.Interfaces.IRepositries;
using Application.Features.Interfaces.IServices;
using Application.Features.Queries.Get;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/Message")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMessageService _messageRepo;
        private readonly IDonationReview _donationReview;
        private readonly IDonationService _donationService;
        public MessageController(IMediator mediator, IMessageService messageRepo, IDonationService donationService, IDonationReview donationReview)
        {
            _mediator = mediator;
            _messageRepo = messageRepo;
            _donationService = donationService;
            _donationReview = donationReview;
        }
       

        [HttpPost("Send")]
        public async Task<IActionResult> SendMessageAsync(CreateMessageCommandModel request)
        {
            var message = await _mediator.Send(request);
            if (message.IsSuccessfull)
            {
                return Ok(message);
            }
            return BadRequest  ();
        }

        [HttpGet("{donationId}")]
        public async Task<IActionResult> GetDonationDetailsWithMessages(Guid donationId)
        {
            var donation = await _donationService.GetDonationByIdAsync(donationId);
            var messages = await _messageRepo.GetMessagesByDonationIdAsync(donationId);

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

        [HttpPost("mark-received")]
        public async Task<IActionResult> MarkDonationAsReceived(MarkReceiveModel request)
        {
            var received = await _messageRepo.DonationReceived(request.DonationId, request.Review);
            if (received.IsSuccessfull)
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpPost("Donation-Available")]
        public async  Task<IActionResult> MarkAvailable(DonationAvailbaleModel request)
        {
            var available = await _messageRepo.ChangeDonationStatusAsync(request.DonationId, request.reason);
            if(available)
            {
                return Ok();
            }
            return BadRequest();
        }
        [HttpPost("Message-Received")]
        public async Task<IActionResult> MarkMessageReceived(Guid donationId)
        {
            var received = await _messageRepo.MarkMessageAsReceivedAsync(donationId);
            if(received)
            {
                return Ok();
            }
            return BadRequest();
        }
        [HttpGet("Message")]
        public async Task<IActionResult> GetMessages(Guid senderId, Guid receiverId)
        {
            var message = await _messageRepo.GetMessagesAsync(senderId, receiverId);
            if(!message.IsSuccessfull)
            {
                return BadRequest();
            }
            return Ok(message.Data);
        }

        [HttpGet("Reviews")]
        public async Task<IActionResult> Reviews()
        {
            var review = await _donationReview.AllUserReview();
            if (review.IsSuccessfull)
            {
                return Ok(review.Data);
            }
            return Ok(review.Data);
        }

        [HttpGet("AllReviews")]
        public async Task<IActionResult> InappropriateReviews()
        {
            var review = await _donationReview.GetAllInappropriateReviews();
            if (review.IsSuccessfull)
            {
                return Ok(review.Data);
            }
            return Ok(review.Data);
        }
    }
}
