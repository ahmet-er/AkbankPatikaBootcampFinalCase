using AFC.Base.Response;
using AFC.Business.Cqrs;
using AFC.Schema;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AFC.Api.Controllers
{
    [Route("api/expense-request")]
    [ApiController]
    public class ExpenseRequestController : ControllerBase
    {
        private readonly IMediator mediator;

        public ExpenseRequestController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        [Authorize(Roles = "Admin, FieldStaff")]
        public async Task<ApiResponse<List<ExpenseRequestResponse>>> Get()
        {
            var operation = new GetAllExpenseRequestQuery();
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpGet("id")]
        [Authorize(Roles = "Admin, FieldStaff")]
        public async Task<ApiResponse<ExpenseRequestResponse>> Get(int id)
        {
            var operation = new GetExpenseRequestByIdQuery(id);
            var result = await mediator.Send(operation);    
            return result;
        }

        [HttpGet("by-parameter")]
        [Authorize(Roles = "Admin, FieldStaff")]
        public async Task<ApiResponse<List<ExpenseRequestResponse>>> GetByParameter(
            [FromQuery] int? FieldStaffId,
            [FromQuery] int? PaymentCategoryId,
            [FromQuery] decimal? MinAmount,
            [FromQuery] decimal? MaxAmount,
            [FromQuery] string? PaymentLocation,
            [FromQuery] string? ExpenseStatus,
            [FromQuery] string? PaymentStatus)
        {
            var operation = new GetExpenseRequestByParameterQuery(FieldStaffId, PaymentCategoryId, MinAmount, MaxAmount, PaymentLocation, ExpenseStatus, PaymentStatus);
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpPost]
        [Authorize(Roles = "FieldStaff")]
        public async Task<ApiResponse<ExpenseRequestResponse>> Post([FromBody] ExpenseRequestByFieldStaffRequest expenseRequest)
        {
            var operation = new CreateExpenseRequestCommand(expenseRequest);
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpPut("by-fieldStaff")]
        [Authorize(Roles = "FieldStaff")]
        public async Task<ApiResponse> PutByFieldStaff(int id, [FromBody] ExpenseRequestByFieldStaffRequest expenseRequest)
        {
            var operation = new UpdateExpenseRequestByFieldStaffCommand(id, expenseRequest);
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpPut("by-admin")]
        [Authorize(Roles = "Admin")]
        public async Task<ApiResponse> PutByAdmin(int id, [FromBody] ExpenseRequestByAdminRequest expenseRequest)
        {
            var operation = new UpdateExpenseRequestByAdminCommand(id, expenseRequest);
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpDelete("id")]
        [Authorize(Roles = "Admin")]
        public async Task<ApiResponse> Delete(int id)
        {
            var operation = new DeleteExpenseRequestCommand(id);
            var result = await mediator.Send(operation);
            return result;
        }
    }
}
