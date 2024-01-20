using AFC.Base.Enums;
using AFC.Base.Response;
using AFC.Business.Cqrs;
using AFC.Business.Helpers;
using AFC.Data;
using AFC.Data.Entity;
using AFC.Schema;
using AutoMapper;
using LinqKit;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace AFC.Business.Query;

public class ExpenseReportQueryHandler :
    IRequestHandler<GetExpenseReportForFieldStaffQuery, ApiResponse<List<ExpenseReportResponse>>>,
    IRequestHandler<GetExpenseReportForAdminQuery, ApiResponse<List<ExpenseReportResponse>>>
{
    private readonly AfcDbContext dbContext;
    private readonly IMapper mapper;
    private readonly IHttpContextAccessor httpContextAccessor;

    public ExpenseReportQueryHandler(AfcDbContext dbContext, IMapper mapper, IHttpContextAccessor httpContextAccessor)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
        this.httpContextAccessor = httpContextAccessor;
    }

    public async Task<ApiResponse<List<ExpenseReportResponse>>> Handle(GetExpenseReportForFieldStaffQuery request, CancellationToken cancellationToken)
    {
        var predicate = PredicateBuilder.New<ExpenseRequest>(true);

        var userId = GetUserIdFromClaims.GetUserId(httpContextAccessor);

        if (userId is 0)
            return new ApiResponse<List<ExpenseReportResponse>>("Invalid query.");

        predicate.And(x => x.FieldStaff.User.Id == userId);

        var list = await dbContext.Set<ExpenseRequest>()
            .Include(x => x.ExpenseDocuments)
            .Include(x => x.PaymentCategory)
            .Include(x => x.FieldStaff)
            .ThenInclude(x => x.User)
            .Where(x => x.IsActive)
            .Where(predicate)
            .ToListAsync(cancellationToken);

        var mappedList = mapper.Map<List<ExpenseRequest>, List<ExpenseReportResponse>>(list);
        return new ApiResponse<List<ExpenseReportResponse>>(mappedList);
    }

    public async Task<ApiResponse<List<ExpenseReportResponse>>> Handle(GetExpenseReportForAdminQuery request, CancellationToken cancellationToken)
    {
        var predicate = PredicateBuilder.New<ExpenseRequest>();

        if (request.DayCount.HasValue)
            predicate.And(x => x.CreateAt >= DateTime.Now.AddDays(request.DayCount.Value));

        if (request.FieldStaffId.HasValue)
            predicate.And(x => x.FieldStaffId ==  request.FieldStaffId);

        if (!string.IsNullOrEmpty(request.ExpenseStatus) && Enum.TryParse<ExpenseStatus>(request.ExpenseStatus, true, out var parsedExpenseStatus))
            predicate.And(x => x.ExpenseStatus == parsedExpenseStatus);
        else if (!string.IsNullOrEmpty(request.ExpenseStatus))
            return new ApiResponse<List<ExpenseReportResponse>>("Invalid ExpenseStatus value.");

        if (request.PaymentCategoryId.HasValue)
            predicate.And(x => x.PaymentCategoryId == request.PaymentCategoryId);

        var list = await dbContext.Set<ExpenseRequest>()
            .Include(x => x.ExpenseDocuments)
            .Include(x => x.PaymentCategory)
            .Include(x => x.FieldStaff)
            .ThenInclude(x => x.User)
            .Where(x => x.IsActive)
            .Where(predicate)
            .ToListAsync(cancellationToken);

        var mappedList = mapper.Map<List<ExpenseRequest>, List<ExpenseReportResponse>>(list);
        return new ApiResponse<List<ExpenseReportResponse>>(mappedList);
    }
}
