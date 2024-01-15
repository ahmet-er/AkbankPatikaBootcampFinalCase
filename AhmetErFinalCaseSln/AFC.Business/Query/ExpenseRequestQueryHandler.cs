using AFC.Base.Response;
using AFC.Business.Cqrs;
using AFC.Schema;
using MediatR;

namespace AFC.Business.Query;

public class ExpenseRequestQueryHandler :
    IRequestHandler<GetAllExpenseRequestQuery, ApiResponse<List<ExpenseRequestResponse>>>,
    IRequestHandler<GetExpenseRequestByIdQuery, ApiResponse<ExpenseRequestResponse>>,
    IRequestHandler<GetExpenseRequestByParameterQuery, ApiResponse<List<ExpenseRequestResponse>>>
{
    public Task<ApiResponse<List<ExpenseRequestResponse>>> Handle(GetAllExpenseRequestQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<ApiResponse<ExpenseRequestResponse>> Handle(GetExpenseRequestByIdQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<ApiResponse<List<ExpenseRequestResponse>>> Handle(GetExpenseRequestByParameterQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
