using ApiServfyNotifications.Application.Handlers.Notification.CommandSendEmail.Command;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ApiServfyNotifications.Controllers.V1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class NotificationController : Controller
    {
        [HttpPost("Email")]
        public async Task<IActionResult> Get([FromServices] IMediator mediator, [FromBody] RequestSendEmail command, CancellationToken cancellationToken)
        {
            var response = await mediator.Send(command, cancellationToken);

            if (!response.success)
                return BadRequest(response);

            return Ok(response);
        }
    }
}
