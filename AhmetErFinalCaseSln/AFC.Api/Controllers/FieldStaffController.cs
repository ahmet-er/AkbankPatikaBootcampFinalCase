using AFC.Base.Response;
using AFC.Business.Cqrs;
using AFC.Schema;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AFC.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FieldStaffController : ControllerBase
    {
        private readonly IMediator mediator;

        public FieldStaffController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        public async Task<ApiResponse<List<FieldStaffResponse>>> Get()
        {
            var operation = new GetAllFieldStaffQuery();
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpGet("id")]
        public async Task<ApiResponse<FieldStaffResponse>> Get(int id)
        {
            var operation = new GetFieldStaffByIdQuery(id);
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpGet("by-parameters")]
        public async Task<ApiResponse<List<FieldStaffResponse>>> GetByParameter(
            [FromQuery] int? UserId,
            [FromQuery] string? IBAN)
        {
            var operation = new GetFieldStaffByParameterQuery(UserId, IBAN);
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpPost]
        public async Task<ApiResponse<FieldStaffResponse>> Post([FromBody] FieldStaffRequest fieldStaff)
        {
            var operation = new CreateFieldStaffCommand(fieldStaff);
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpPut("id")]
        public async Task<ApiResponse> Put(int id, [FromBody] FieldStaffRequest fieldStaff)
        {
            var operation = new UpdateFieldStaffCommand(id, fieldStaff);
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpDelete("id")]
        public async Task<ApiResponse> Delete(int id)
        {
            var operation = new DeleteFieldStaffCommand(id);
            var result = await mediator.Send(operation);
            return result;
        }
    }
}
