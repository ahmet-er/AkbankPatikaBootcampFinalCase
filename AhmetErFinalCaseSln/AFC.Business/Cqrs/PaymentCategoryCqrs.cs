using AFC.Base.Response;
using AFC.Schema;
using MediatR;

namespace AFC.Business.Cqrs;

public record CreatePaymentCategoryCommand(PaymentCategoryRequest Model) : IRequest<ApiResponse<PaymentCategoryResponse>>;
public record UpdatePaymentCategoryCommand(int Id, PaymentCategoryRequest Model) : IRequest<ApiResponse>;
public record DeletePaymentCategoryCommand(int Id)  : IRequest<ApiResponse>;

public record GetAllPaymentCategoryQuery() : IRequest<ApiResponse<List<PaymentCategoryResponse>>>;
public record GetPaymentCategoryById(int Id) : IRequest<ApiResponse<PaymentCategoryResponse>>;
public record GetPaymentCategoryByParameter(string Name) : IRequest<ApiResponse<List<PaymentCategoryResponse>>>;