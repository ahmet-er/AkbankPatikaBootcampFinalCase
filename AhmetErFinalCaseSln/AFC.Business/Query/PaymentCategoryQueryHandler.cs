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

public class PaymentCategoryQueryHandler :
    IRequestHandler<GetAllPaymentCategoryQuery, ApiResponse<List<PaymentCategoryResponse>>>,
    IRequestHandler<GetPaymentCategoryById, ApiResponse<PaymentCategoryResponse>>,
    IRequestHandler<GetPaymentCategoryByParameter, ApiResponse<List<PaymentCategoryResponse>>>
{
    private readonly AfcDbContext dbContext;
    private readonly IMapper mapper;

    public PaymentCategoryQueryHandler(AfcDbContext dbContext, IMapper mapper)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }

    public async Task<ApiResponse<List<PaymentCategoryResponse>>> Handle(GetAllPaymentCategoryQuery request, CancellationToken cancellationToken)
    {
        var list = await dbContext.Set<PaymentCategory>()
            .Where(x => x.IsActive)
            .ToListAsync(cancellationToken);

        var mappedList = mapper.Map<List<PaymentCategory>, List<PaymentCategoryResponse>>(list);
        return new ApiResponse<List<PaymentCategoryResponse>>(mappedList);
    }

    public async Task<ApiResponse<PaymentCategoryResponse>> Handle(GetPaymentCategoryById request, CancellationToken cancellationToken)
    {
        var entity = await dbContext.Set<PaymentCategory>()
            .Where(x => x.Id == request.Id && x.IsActive)
            .FirstOrDefaultAsync(cancellationToken);

        if (entity is null)
            return new ApiResponse<PaymentCategoryResponse>("Record not found.");

        var mapped = mapper.Map<PaymentCategory, PaymentCategoryResponse>(entity);
        return new ApiResponse<PaymentCategoryResponse>(mapped);
    }

    public async Task<ApiResponse<List<PaymentCategoryResponse>>> Handle(GetPaymentCategoryByParameter request, CancellationToken cancellationToken)
    {
        var predicate = PredicateBuilder.New<PaymentCategory>(true);

        if (string.IsNullOrEmpty(request.Name))
            predicate.And(x => x.Name.ToUpper().Contains(request.Name.ToUpper()));

        var list = await dbContext.Set<PaymentCategory>()
            .Where(x => x.IsActive)
            .Where(predicate)
            .ToListAsync(cancellationToken);

        var mappedList = mapper.Map<List<PaymentCategory>, List<PaymentCategoryResponse>>(list);
        return new ApiResponse<List<PaymentCategoryResponse>>(mappedList);
    }
}
