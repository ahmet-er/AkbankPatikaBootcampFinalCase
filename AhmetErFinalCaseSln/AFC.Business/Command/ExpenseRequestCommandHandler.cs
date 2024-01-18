using AFC.Base.Enums;
using AFC.Base.Response;
using AFC.Business.Cqrs;
using AFC.Business.Helpers;
using AFC.Data;
using AFC.Data.Entity;
using AFC.Schema;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace AFC.Business.Command;

public class ExpenseRequestCommandHandler :
    IRequestHandler<CreateExpenseRequestCommand, ApiResponse<ExpenseRequestResponse>>,
    IRequestHandler<UpdateExpenseRequestByFieldStaffCommand, ApiResponse>,
    IRequestHandler<UpdateExpenseRequestByAdminCommand, ApiResponse>,
    IRequestHandler<DeleteExpenseRequestCommand, ApiResponse>
{
    private readonly AfcDbContext dbContext;
    private readonly IMapper mapper;
    private readonly IHttpContextAccessor httpContextAccessor;

    public ExpenseRequestCommandHandler(AfcDbContext dbContext, IMapper mapper, IHttpContextAccessor httpContextAccessor)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
        this.httpContextAccessor = httpContextAccessor;
    }

    public async Task<ApiResponse<ExpenseRequestResponse>> Handle(CreateExpenseRequestCommand request, CancellationToken cancellationToken)
    {
        var entity = mapper.Map<ExpenseRequestByFieldStaffRequest, ExpenseRequest>(request.Model);

        BaseEntitySetPropertyExtension.SetCreatedProperties(entity, httpContextAccessor);

        entity.PaymentStatus = PaymentStatus.Unpaid;
        entity.ExpenseStatus = ExpenseStatus.Waiting;

        var entityResult = await dbContext.AddAsync(entity, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);

        var mapped = mapper.Map<ExpenseRequest, ExpenseRequestResponse>(entityResult.Entity);
        return new ApiResponse<ExpenseRequestResponse>(mapped);
    }

    public async Task<ApiResponse> Handle(UpdateExpenseRequestByFieldStaffCommand request, CancellationToken cancellationToken)
    {
        var fromdb = await dbContext.Set<ExpenseRequest>()
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (fromdb is null)
            return new ApiResponse("Record not found.");

        fromdb.Amount = request.Model.Amount;
        fromdb.Description = request.Model.Description;
        fromdb.PaymentLocation = request.Model.PaymentLocation;

        BaseEntitySetPropertyExtension.SetModifiedProperties(fromdb, httpContextAccessor);

        await dbContext.SaveChangesAsync(cancellationToken);
        return new ApiResponse();
    }

    public async Task<ApiResponse> Handle(UpdateExpenseRequestByAdminCommand request, CancellationToken cancellationToken)
    {
        var fromdb = await dbContext.Set<ExpenseRequest>()
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (fromdb is null)
            return new ApiResponse("Record not found.");

        fromdb.CompanyResultDescription = request.Model.CompanyResultDescription;
        fromdb.ExpenseStatus = request.Model.ExpenseStatus;
        fromdb.PaymentStatus = request.Model.PaymentStatus;

        BaseEntitySetPropertyExtension.SetModifiedProperties(fromdb, httpContextAccessor);

        await dbContext.SaveChangesAsync(cancellationToken);
        return new ApiResponse();
    }

    public async Task<ApiResponse> Handle(DeleteExpenseRequestCommand request, CancellationToken cancellationToken)
    {
        var fromdb = await dbContext.Set<ExpenseRequest>()
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (fromdb is null)
            return new ApiResponse("Record not found.");

        fromdb.IsActive = false;
        await dbContext.SaveChangesAsync(cancellationToken);
        return new ApiResponse();
    }
}
