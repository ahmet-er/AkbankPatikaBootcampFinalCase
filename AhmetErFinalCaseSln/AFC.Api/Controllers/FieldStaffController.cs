using AFC.Base.Response;
using AFC.Business.Cqrs;
using AFC.Schema;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AFC.Api.Controllers
{
    [Route("api/field-staff")]
    [ApiController]
    public class FieldStaffController : ControllerBase
    {
        private readonly IMediator mediator;

        public FieldStaffController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        [Authorize(Roles = "Admin, FieldStaff")]
        public async Task<ApiResponse<List<FieldStaffResponse>>> Get()
        {
            var operation = new GetAllFieldStaffQuery();
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpGet("id")]
        [Authorize(Roles = "Admin, FieldStaff")]
        public async Task<ApiResponse<FieldStaffResponse>> Get(int id)
        {
            var operation = new GetFieldStaffByIdQuery(id);
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpGet("by-parameters")]
        [Authorize(Roles = "Admin, FieldStaff")]
        public async Task<ApiResponse<List<FieldStaffResponse>>> GetByParameter(
            [FromQuery] int? UserId,
            [FromQuery] string? IBAN)
        {
            var operation = new GetFieldStaffByParameterQuery(UserId, IBAN);
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ApiResponse<FieldStaffResponse>> Post([FromBody] FieldStaffRequest fieldStaff)
        {
            var operation = new CreateFieldStaffCommand(fieldStaff);
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpPut("id")]
        [Authorize(Roles = "Admin")]
        public async Task<ApiResponse> Put(int id, [FromBody] UpdateFieldStaffRequest fieldStaff)
        {
            var operation = new UpdateFieldStaffCommand(id, fieldStaff);
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpDelete("id")]
        [Authorize(Roles = "Admin")]
        public async Task<ApiResponse> Delete(int id)
        {
            var operation = new DeleteFieldStaffCommand(id);
            var result = await mediator.Send(operation);
            return result;
        }
    }
}
