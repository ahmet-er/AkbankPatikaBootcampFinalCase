using AFC.Base.Response;
using AFC.Schema;
using MediatR;

namespace AFC.Business.Cqrs;

#region Commands
public record CreatePaymentCategoryCommand(PaymentCategoryRequest Model) : IRequest<ApiResponse<PaymentCategoryResponse>>;
public record UpdatePaymentCategoryCommand(int Id, PaymentCategoryRequest Model) : IRequest<ApiResponse>;
public record DeletePaymentCategoryCommand(int Id) : IRequest<ApiResponse>;

#endregion
#region Queries
public record GetAllPaymentCategoryQuery() : IRequest<ApiResponse<List<PaymentCategoryResponse>>>;
public record GetPaymentCategoryByIdQuery(int Id) : IRequest<ApiResponse<PaymentCategoryResponse>>;
public record GetPaymentCategoryByParameterQuery(string Name) : IRequest<ApiResponse<List<PaymentCategoryResponse>>>; 
#endregion