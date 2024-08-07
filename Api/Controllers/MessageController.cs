using Application.Features.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/Message")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly IMediator _mediator;
        public MessageController(IMediator mediator)
        {
            _mediator = mediator;
        }
        //public IActionResult Index()
        //{
        //    return View();
        //}

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
    }
}
