using AFC.Base.Response;
using AFC.Business.Cqrs;
using AFC.Schema;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AFC.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpenseRequestController : ControllerBase
    {
        private readonly IMediator mediator;

        public ExpenseRequestController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        public async Task<ApiResponse<List<ExpenseRequestResponse>>> Get()
        {
            var operation = new GetAllExpenseRequestQuery();
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpGet("id")]
        public async Task<ApiResponse<ExpenseRequestResponse>> Get(int id)
        {
            var operation = new GetExpenseRequestByIdQuery(id);
            var result = await mediator.Send(operation);    
            return result;
        }

        [HttpGet("by-parameter")]
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
        public async Task<ApiResponse<ExpenseRequestResponse>> Post([FromBody] ExpenseRequestRequest expenseRequest)
        {
            var operation = new CreateExpenseRequestCommand(expenseRequest);
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpPut("id")]
        public async Task<ApiResponse> Put(int id, [FromBody] ExpenseRequestRequest expenseRequest)
        {
            var operation = new UpdateExpenseRequestCommand(id, expenseRequest);
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpDelete("id")]
        public async Task<ApiResponse> Delete(int id)
        {
            var operation = new DeleteExpenseRequestCommand(id);
            var result = await mediator.Send(operation);
            return result;
        }
    }
}
