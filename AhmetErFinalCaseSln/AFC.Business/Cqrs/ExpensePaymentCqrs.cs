using AFC.Base.Response;
using AFC.Schema;
using MediatR;

namespace AFC.Business.Cqrs;

public record CreateExpensePaymentCommand(ExpensePaymentRequest Model) : IRequest<ApiResponse<ExpensePaymentResponse>>;