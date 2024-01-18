using AFC.Base.Enums;
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

public class ExpenseRequestQueryHandler :
    IRequestHandler<GetAllExpenseRequestQuery, ApiResponse<List<ExpenseRequestResponse>>>,
    IRequestHandler<GetExpenseRequestByIdQuery, ApiResponse<ExpenseRequestResponse>>,
    IRequestHandler<GetExpenseRequestByParameterQuery, ApiResponse<List<ExpenseRequestResponse>>>
{
    private readonly AfcDbContext dbContext;
    private readonly IMapper mapper;

    public ExpenseRequestQueryHandler(AfcDbContext dbContext, IMapper mapper)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }

    public async Task<ApiResponse<List<ExpenseRequestResponse>>> Handle(GetAllExpenseRequestQuery request, CancellationToken cancellationToken)
    {
        var list = await dbContext.Set<ExpenseRequest>()
            .Include(x => x.ExpenseDocuments)
            .Where(x => x.IsActive)
            .ToListAsync(cancellationToken);

        var mappedList = mapper.Map<List<ExpenseRequest>, List<ExpenseRequestResponse>>(list);
        return new ApiResponse<List<ExpenseRequestResponse>>(mappedList);
    }

    public async Task<ApiResponse<ExpenseRequestResponse>> Handle(GetExpenseRequestByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await dbContext.Set<ExpenseRequest>()
            .Include(x => x.ExpenseDocuments)
            .Where(x => x.IsActive)
            .FirstOrDefaultAsync(cancellationToken);

        if (entity is null)
            return new ApiResponse<ExpenseRequestResponse>("Record not found.");

        var mapped = mapper.Map<ExpenseRequest, ExpenseRequestResponse>(entity);
        return new ApiResponse<ExpenseRequestResponse>(mapped);
    }

    public async Task<ApiResponse<List<ExpenseRequestResponse>>> Handle(GetExpenseRequestByParameterQuery request, CancellationToken cancellationToken)
    {
        var predicate = PredicateBuilder.New<ExpenseRequest>(true);

        if (string.IsNullOrEmpty(request.PaymentLocation))
            predicate.And(x => x.PaymentLocation.ToUpper().Contains(request.PaymentLocation.ToUpper()));

        if (Enum.TryParse<ExpenseStatus>(request.ExpenseStatus, true, out var parsedExpenseStatus))
            predicate.And(x => x.ExpenseStatus == parsedExpenseStatus);
        else
            return new ApiResponse<List<ExpenseRequestResponse>>("Invalid expense status type.");

        if (Enum.TryParse<PaymentStatus>(request.PaymentStatus, true, out var parsedPaymentStatus))
            predicate.And(x => x.PaymentStatus == parsedPaymentStatus);
        else
            return new ApiResponse<List<ExpenseRequestResponse>>("Invalid payment status type.");

        var list = await dbContext.Set<ExpenseRequest>()
            .Include(x => x.ExpenseDocuments)
            .Where(x => x.IsActive)
            .ToListAsync(cancellationToken);
        
        var mappedList = mapper.Map<List<ExpenseRequest>, List<ExpenseRequestResponse>>(list);
        return new ApiResponse<List<ExpenseRequestResponse>>(mappedList);
    }
}
