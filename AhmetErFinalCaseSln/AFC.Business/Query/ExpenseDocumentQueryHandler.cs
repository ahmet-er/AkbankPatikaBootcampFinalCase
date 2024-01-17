using AFC.Base.Response;
using AFC.Business.Cqrs;
using AFC.Data;
using AFC.Data.Entity;
using AFC.Schema;
using AutoMapper;
using LinqKit;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AFC.Business.Query;

public class ExpenseDocumentQueryHandler :
    IRequestHandler<GetAllExpenseDocumentQuery, ApiResponse<List<ExpenseDocumentResponse>>>,
    IRequestHandler<GetExpenseDocumentByIdQuery, ApiResponse<ExpenseDocumentResponse>>,
    IRequestHandler<GetExpenseDocumentByParameterQuery, ApiResponse<List<ExpenseDocumentResponse>>>
{
    private readonly AfcDbContext dbContext;
    private readonly IMapper mapper;

    public ExpenseDocumentQueryHandler(AfcDbContext dbContext, IMapper mapper)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }

    public async Task<ApiResponse<List<ExpenseDocumentResponse>>> Handle(GetAllExpenseDocumentQuery request, CancellationToken cancellationToken)
    {
        var list = await dbContext.Set<ExpenseDocument>()
            .Include(x => x.ExpenseRequest)
            .Where(x => x.IsActive)
            .ToListAsync(cancellationToken);

        var mappedList = mapper.Map<List<ExpenseDocument>, List<ExpenseDocumentResponse>>(list);
        return new ApiResponse<List<ExpenseDocumentResponse>>(mappedList);
    }

    public async Task<ApiResponse<ExpenseDocumentResponse>> Handle(GetExpenseDocumentByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await dbContext.Set<ExpenseDocument>()
            .Include(x => x.ExpenseRequest)
            .FirstOrDefaultAsync(x => x.Id ==  request.Id, cancellationToken);

        if (entity is null)
            return new ApiResponse<ExpenseDocumentResponse>("Record not found.");

        var mapped = mapper.Map<ExpenseDocument, ExpenseDocumentResponse>(entity);
        return new ApiResponse<ExpenseDocumentResponse>(mapped);
    }

    public async Task<ApiResponse<List<ExpenseDocumentResponse>>> Handle(GetExpenseDocumentByParameterQuery request, CancellationToken cancellationToken)
    {
        var predicate = PredicateBuilder.New<ExpenseDocument>(true);

        if (string.IsNullOrEmpty(request.FileType))
            predicate.And(x => x.FileType.ToUpper().Contains(request.FileType.ToUpper()));

        if (string.IsNullOrEmpty(request.FileName))
            predicate.And(x => x.FileName.ToUpper().Contains(request.FileName.ToUpper()));

        var list = await dbContext.Set<ExpenseDocument>()
            .Include(x => x.ExpenseRequest)
            .Where(x => x.IsActive)
            .Where(predicate)
            .ToListAsync(cancellationToken);

        var mappedList = mapper.Map<List<ExpenseDocument>, List<ExpenseDocumentResponse>>(list);
        return new ApiResponse<List<ExpenseDocumentResponse>>(mappedList);
    }
}
