using AFC.Base.Response;
using AFC.Business.Cqrs;
using AFC.Schema;
using MediatR;

namespace AFC.Business.Command;

public class ExpenseDocumentCommandHandler :
    IRequestHandler<CreateExpenseDocumentCommand, ApiResponse<ExpenseDocumentResponse>>,
    IRequestHandler<UpdateExpenseDocumentCommand, ApiResponse>,
    IRequestHandler<DeleteExpenseDocumentCommand, ApiResponse>
{
    public Task<ApiResponse<ExpenseDocumentResponse>> Handle(CreateExpenseDocumentCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<ApiResponse> Handle(UpdateExpenseDocumentCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<ApiResponse> Handle(DeleteExpenseDocumentCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
