using AFC.Base.Response;
using AFC.Business.Cqrs;
using AFC.Schema;
using MediatR;

namespace AFC.Business.Command;

public class PaymentCategoryCommandHandler :
    IRequestHandler<CreatePaymentCategoryCommand, ApiResponse<PaymentCategoryResponse>>,
    IRequestHandler<UpdatePaymentCategoryCommand, ApiResponse>,
    IRequestHandler<DeletePaymentCategoryCommand, ApiResponse>
{
    public Task<ApiResponse<PaymentCategoryResponse>> Handle(CreatePaymentCategoryCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<ApiResponse> Handle(UpdatePaymentCategoryCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<ApiResponse> Handle(DeletePaymentCategoryCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
