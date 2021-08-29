using Divagando.Domain.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Divagando.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class PaymentGatewayController : ControllerBase
    {
        private readonly IEventService _eventService;

        public PaymentGatewayController(IEventService eventService)
        {
            _eventService = eventService;
        }

        [HttpPost("Notifications")]
        public IActionResult Notifications(string topic, long id)
        {
            if (topic == "payment")
            {
                _eventService.UpdateParticipation(id);
            }
            return Ok();
        }
    }
}
