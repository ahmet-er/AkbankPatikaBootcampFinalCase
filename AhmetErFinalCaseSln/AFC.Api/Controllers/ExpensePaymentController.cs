using AFC.Base.Response;
using AFC.Business.Cqrs;
using AFC.Schema;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AFC.Api.Controllers;

[Route("api/expense-payments")]
[ApiController]
public class ExpensePaymentController : ControllerBase
{
    private readonly IMediator mediator;

    public ExpensePaymentController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<ApiResponse<ExpensePaymentResponse>> Payment([FromBody]ExpensePaymentRequest expensePayment)
    {
        var operation = new CreateExpensePaymentCommand(expensePayment);
        var result = await mediator.Send(operation);
        return result;
    }
}
