using AFC.Base.Response;
using AFC.Business.Cqrs;
using AFC.Data;
using AFC.Schema;
using AutoMapper;
using MediatR;

namespace AFC.Business.Command;

public class ExpenseDocumentCommandHandler :
    IRequestHandler<CreateExpenseDocumentCommand, ApiResponse<ExpenseDocumentResponse>>,
    IRequestHandler<UpdateExpenseDocumentCommand, ApiResponse>,
    IRequestHandler<DeleteExpenseDocumentCommand, ApiResponse>
{
    private readonly AfcDbContext dbContext;
    private readonly IMapper mapper;

    public ExpenseDocumentCommandHandler(AfcDbContext dbContext, IMapper mapper)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }

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
