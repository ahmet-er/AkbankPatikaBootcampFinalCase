using AFC.Base.Response;
using AFC.Business.Cqrs;
using AFC.Schema;
using MediatR;

namespace AFC.Business.Query;

public class ExpenseDocumentQueryHandler :
    IRequestHandler<GetAllExpenseDocumentQuery, ApiResponse<List<ExpenseDocumentResponse>>>,
    IRequestHandler<GetExpenseDocumentByIdQuery, ApiResponse<ExpenseDocumentResponse>>,
    IRequestHandler<GetExpenseDocumentByParameterQuery, ApiResponse<List<ExpenseDocumentResponse>>>
{
    public Task<ApiResponse<List<ExpenseDocumentResponse>>> Handle(GetAllExpenseDocumentQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<ApiResponse<ExpenseDocumentResponse>> Handle(GetExpenseDocumentByIdQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<ApiResponse<List<ExpenseDocumentResponse>>> Handle(GetExpenseDocumentByParameterQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
