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

public class FieldStaffQueryHandler :
    IRequestHandler<GetAllFieldStaffQuery, ApiResponse<List<FieldStaffResponse>>>,
    IRequestHandler<GetFieldStaffByIdQuery, ApiResponse<FieldStaffResponse>>,
    IRequestHandler<GetFieldStaffByParameterQuery, ApiResponse<List<FieldStaffResponse>>>
{
    private readonly AfcDbContext dbContext;
    private readonly IMapper mapper;

    public FieldStaffQueryHandler(AfcDbContext dbContext, IMapper mapper)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }

    public async Task<ApiResponse<List<FieldStaffResponse>>> Handle(GetAllFieldStaffQuery request, CancellationToken cancellationToken)
    {
        var list = await dbContext.Set<FieldStaff>()
            .Include(x => x.User)
            .Include(x => x.ExpenseRequests)
            .Where(x => x.IsActive)
            .ToListAsync(cancellationToken);

        var mappedList = mapper.Map<List<FieldStaff>, List<FieldStaffResponse>>(list);
        return new ApiResponse<List<FieldStaffResponse>>(mappedList);
    }

    public async Task<ApiResponse<FieldStaffResponse>> Handle(GetFieldStaffByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await dbContext.Set<FieldStaff>()
            .Include(x => x.User)
            .FirstOrDefaultAsync(x => x.IsActive && x.Id == request.Id, cancellationToken);

        if (entity is null)
            return new ApiResponse<FieldStaffResponse>("Record not found.");

        var mapped = mapper.Map<FieldStaff, FieldStaffResponse>(entity);
        return new ApiResponse<FieldStaffResponse>(mapped);
    }

    public async Task<ApiResponse<List<FieldStaffResponse>>> Handle(GetFieldStaffByParameterQuery request, CancellationToken cancellationToken)
    {
        var predicate = PredicateBuilder.New<FieldStaff>(true);

        if (request.UserId.HasValue)
            predicate.And(x => x.UserId == request.UserId);

        if (!string.IsNullOrEmpty(request.IBAN))
            predicate.And(x => x.IBAN.Contains(request.IBAN));

        var list = await dbContext.Set<FieldStaff>()
            .Include(x => x.User)
            .Include(x => x.ExpenseRequests)
            .Where(x => x.IsActive)
            .Where(predicate)
            .ToListAsync(cancellationToken);

        var mappedList = mapper.Map<List<FieldStaff>, List<FieldStaffResponse>>(list);
        return new ApiResponse<List<FieldStaffResponse>>(mappedList);
    }
}
