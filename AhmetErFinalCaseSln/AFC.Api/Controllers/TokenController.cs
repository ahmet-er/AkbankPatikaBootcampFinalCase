using AFC.Base.Response;
using AFC.Business.Cqrs;
using AFC.Schema;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AFC.Api.Controllers
{
    [Route("api/token")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly IMediator mediator;

        public TokenController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        public async Task<ApiResponse<TokenResponse>> Post([FromBody] TokenRequest request)
        {
            var operation = new CreateTokenCommand(request);
            var result = await mediator.Send(operation);
            return result;
        }
    }
}
