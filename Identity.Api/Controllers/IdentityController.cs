using Identity.Api.Commands;
using Identity.Api.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Mvc;


namespace Identity.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IdentityController : ControllerBase
    {
        private readonly IMediator _mediator;
        public IdentityController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("/login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestCommand request)
        {
            try 
            {
                var token = await _mediator.Send(request);
                return Ok(token);
            }
            catch (UserNotFoundException)
            {
                // do something
                return NotFound();
            }
            catch (UserNotAuthorizedException)
            {
                // do something
                return Unauthorized();
            }
        }

    }
}
