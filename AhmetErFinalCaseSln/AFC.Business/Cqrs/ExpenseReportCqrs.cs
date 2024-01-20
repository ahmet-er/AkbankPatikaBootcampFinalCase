using AFC.Base.Response;
using AFC.Schema;
using MediatR;

namespace AFC.Business.Cqrs;

public record GetExpenseReportForFieldStaffQuery() : IRequest<ApiResponse<List<ExpenseReportResponse>>>;
public record GetExpenseReportForAdminQuery(int? DayCount, int? FieldStaffId, string ExpenseStatus, int? PaymentCategoryId) : IRequest<ApiResponse<List<ExpenseReportResponse>>>;
