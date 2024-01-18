using AFC.Base.Response;
using AFC.Business.Cqrs;
using AFC.Schema;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AFC.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpenseDocumentController : ControllerBase
    {
        private readonly IMediator mediator;

        public ExpenseDocumentController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        public async Task<ApiResponse<List<ExpenseDocumentResponse>>> Get()
        {
            var operation = new GetAllExpenseDocumentQuery();
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpGet("id")]
        public async Task<ApiResponse<ExpenseDocumentResponse>> Get(int id)
        {
            var operation = new GetExpenseDocumentByIdQuery(id);
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpGet("by-parameter")]
        public async Task<ApiResponse<List<ExpenseDocumentResponse>>> GetByParameter(
            [FromQuery] int? ExpenseRequestId,
            [FromQuery] string? FileType,
            [FromQuery] string? FileName)
        {
            var operation = new GetExpenseDocumentByParameterQuery(ExpenseRequestId, FileType, FileName);
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpPost]
        public async Task<ApiResponse<ExpenseDocumentResponse>> Post([FromBody] ExpenseDocumentRequest expenseDocument)
        {
            var operation = new CreateExpenseDocumentCommand(expenseDocument);
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpPut("id")]
        public async Task<ApiResponse<ExpenseDocumentResponse>> Put(int id, [FromBody] ExpenseDocumentRequest expenseDocument)
        {
            var operation = new UpdateExpenseDocumentCommand(id, expenseDocument);
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpDelete("id")]
        public async Task<ApiResponse> Delete(int id)
        {
            var operation = new DeleteExpenseDocumentCommand(id);
            var result = await mediator.Send(operation);
            return result;
        }
    }
}
