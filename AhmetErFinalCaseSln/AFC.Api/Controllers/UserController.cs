using AFC.Base.Enums;
using AFC.Base.Response;
using AFC.Business.Cqrs;
using AFC.Schema;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AFC.Api.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator mediator;

        public UserController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        public async Task<ApiResponse<List<UserResponse>>> Get()
        {
            var operation = new GetAllUserQuery();
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpGet("id")]
        public async Task<ApiResponse<UserResponse>> Get(int id)
        {
            var operation = new GetUserById(id);
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpGet("by-parameters")]
        public async Task<ApiResponse<List<UserResponse>>> GetByParameter(
            [FromQuery] string? UserName,
            [FromQuery] string? FirstName,
            [FromQuery] string? LastName,
            [FromQuery] string? Email,
            [FromQuery] string? Role)
        {
            var operation = new GetUserByParameter(UserName, FirstName, LastName, Email, Role);
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpPost]
        public async Task<ApiResponse<UserResponse>> Post([FromBody] UserRequest user)
        {
            var operation = new CreateUserCommand(user);
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpPut("id")]
        public async Task<ApiResponse> Put(int id, [FromBody] UserRequest user)
        {
            var operation = new UpdateUserCommand(id, user);
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpDelete("id")]
        public async Task<ApiResponse> Delete(int id)
        {
            var operation = new DeleteUserCommand(id);
            var result = await mediator.Send(operation);
            return result;
        }
    }
}
