using AFC.Base.Response;
using AFC.Business.Cqrs;
using AFC.Schema;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AFC.Api.Controllers
{
    [Route("api/expense-documents")]
    [ApiController]
    public class ExpenseDocumentController : ControllerBase
    {
        private readonly IMediator mediator;

        public ExpenseDocumentController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        [Authorize(Roles = "Admin, FieldStaff")]
        public async Task<ApiResponse<List<ExpenseDocumentResponse>>> Get()
        {
            var operation = new GetAllExpenseDocumentQuery();
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpGet("id")]
        [Authorize(Roles = "Admin, FieldStaff")]
        public async Task<ApiResponse<ExpenseDocumentResponse>> Get(int id)
        {
            var operation = new GetExpenseDocumentByIdQuery(id);
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpGet("by-parameter")]
        [Authorize(Roles = "Admin, FieldStaff")]
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
        [Authorize(Roles = "Admin, FieldStaff")]
        public async Task<ApiResponse<ExpenseDocumentResponse>> Post([FromQuery] int ExpenseRequestId, IFormFile FormFile)
        {
            var operation = new CreateExpenseDocumentCommand(ExpenseRequestId, FormFile);
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpPut("id")]
        [Authorize(Roles = "Admin, FieldStaff")]
        public async Task<ApiResponse<ExpenseDocumentResponse>> Put(int id, [FromBody] UpdateExpenseDocumentRequest expenseDocument)
        {
            var operation = new UpdateExpenseDocumentCommand(id, expenseDocument);
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpDelete("id")]
        [Authorize(Roles = "Admin")]
        public async Task<ApiResponse> Delete(int id)
        {
            var operation = new DeleteExpenseDocumentCommand(id);
            var result = await mediator.Send(operation);
            return result;
        }
    }
}
