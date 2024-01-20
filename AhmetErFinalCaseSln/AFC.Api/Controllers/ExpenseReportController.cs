using AFC.Base.Response;
using AFC.Business.Cqrs;
using AFC.Schema;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AFC.Api.Controllers;

[Route("api/expense-report")]
[ApiController]
public class ExpenseReportController : ControllerBase
{
    private readonly IMediator mediator;

    public ExpenseReportController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [HttpGet("for-field-staff")]
    [Authorize(Roles = "FieldStaff")]
    public async Task<ApiResponse<List<ExpenseReportResponse>>> Get()
    {
        var operation = new GetExpenseReportForFieldStaffQuery();
        var result = await mediator.Send(operation);
        return result;
    }

    [HttpGet("for-admin")]
    [Authorize(Roles = "Admin")]
    public async Task<ApiResponse<List<ExpenseReportResponse>>> Get(
        [FromQuery] int? DayCount,
        [FromQuery] int? FieldStaffId,
        [FromQuery] string? ExpenseStatus,
        [FromQuery] int? PaymentCategoryId)
    {
        var operation = new GetExpenseReportForAdminQuery(DayCount, FieldStaffId, ExpenseStatus, PaymentCategoryId);
        var result = await mediator.Send(operation);
        return result;
    }
}
