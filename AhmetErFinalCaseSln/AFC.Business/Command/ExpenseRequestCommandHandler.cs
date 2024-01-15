using AFC.Base.Response;
using AFC.Business.Cqrs;
using AFC.Schema;
using MediatR;

namespace AFC.Business.Command;

public class ExpenseRequestCommandHandler :
    IRequestHandler<CreateExpenseRequestCommand, ApiResponse<ExpenseRequestResponse>>,
    IRequestHandler<UpdateExpenseRequestCommand, ApiResponse>,
    IRequestHandler<DeleteExpenseRequestCommand, ApiResponse>
{
    public Task<ApiResponse<ExpenseRequestResponse>> Handle(CreateExpenseRequestCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<ApiResponse> Handle(UpdateExpenseRequestCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<ApiResponse> Handle(DeleteExpenseRequestCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
