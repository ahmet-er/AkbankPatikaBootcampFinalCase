using AFC.Base.Response;
using AFC.Schema;
using MediatR;

namespace AFC.Business.Cqrs;

#region Commands
public record CreateExpenseRequestCommand(ExpenseRequestByFieldStaffRequest Model) : IRequest<ApiResponse<ExpenseRequestResponse>>;
public record UpdateExpenseRequestByFieldStaffCommand(int Id, UpdateExpenseRequestByFieldStaffRequest Model) : IRequest<ApiResponse>;
public record UpdateExpenseRequestByAdminCommand(int Id, UpdateExpenseRequestByAdminRequest Model) : IRequest<ApiResponse>;
public record DeleteExpenseRequestCommand(int Id) : IRequest<ApiResponse>;

#endregion
#region Queries
public record GetAllExpenseRequestQuery() : IRequest<ApiResponse<List<ExpenseRequestResponse>>>;
public record GetExpenseRequestByIdQuery(int Id) : IRequest<ApiResponse<ExpenseRequestResponse>>;
public record GetExpenseRequestByParameterQuery(int? FieldStaffId, int? PaymentCategoryId, decimal? MinAmount, decimal? MaxAmount, string PaymentLocation, string ExpenseStatus, string PaymentStatus) : IRequest<ApiResponse<List<ExpenseRequestResponse>>>; 
#endregion