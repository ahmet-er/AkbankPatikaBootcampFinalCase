using AFC.Base.Response;
using AFC.Business.Cqrs;
using AFC.Schema;
using MediatR;

namespace AFC.Business.Query;

public class PaymentCategoryQueryHandler :
    IRequestHandler<GetAllPaymentCategoryQuery, ApiResponse<List<PaymentCategoryResponse>>>,
    IRequestHandler<GetPaymentCategoryById, ApiResponse<PaymentCategoryResponse>>,
    IRequestHandler<GetPaymentCategoryByParameter, ApiResponse<List<PaymentCategoryResponse>>>
{
    public Task<ApiResponse<List<PaymentCategoryResponse>>> Handle(GetAllPaymentCategoryQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<ApiResponse<PaymentCategoryResponse>> Handle(GetPaymentCategoryById request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<ApiResponse<List<PaymentCategoryResponse>>> Handle(GetPaymentCategoryByParameter request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
