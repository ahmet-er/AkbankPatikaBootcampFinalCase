using AFC.Base.Response;
using AFC.Business.Cqrs;
using AFC.Data;
using AFC.Data.Entity;
using AFC.Schema;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AFC.Business.Command;

public class ExpenseRequestCommandHandler :
    IRequestHandler<CreateExpenseRequestCommand, ApiResponse<ExpenseRequestResponse>>,
    IRequestHandler<UpdateExpenseRequestCommand, ApiResponse>,
    IRequestHandler<DeleteExpenseRequestCommand, ApiResponse>
{
    private readonly AfcDbContext dbContext;
    private readonly IMapper mapper;

    public ExpenseRequestCommandHandler(AfcDbContext dbContext, IMapper mapper)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }

    public async Task<ApiResponse<ExpenseRequestResponse>> Handle(CreateExpenseRequestCommand request, CancellationToken cancellationToken)
    {
        var entity = mapper.Map<ExpenseRequestRequest, ExpenseRequest>(request.Model);

        var entityResult = await dbContext.AddAsync(entity, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);

        var mapped = mapper.Map<ExpenseRequest, ExpenseRequestResponse>(entityResult.Entity);
        return new ApiResponse<ExpenseRequestResponse>(mapped);
    }

    public async Task<ApiResponse> Handle(UpdateExpenseRequestCommand request, CancellationToken cancellationToken)
    {
        var fromdb = await dbContext.Set<ExpenseRequest>()
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (fromdb is null)
            return new ApiResponse("Record not found.");

        fromdb.Amount = request.Model.Amount;
        fromdb.Description = request.Model.Description;
        fromdb.CompanyResultDescription = request.Model.CompanyResultDescription;
        fromdb.PaymentLocation = request.Model.PaymentLocation;
        //fromdb.ExpenseStatus = request.Model.ExpenseStatus;
        //fromdb.PaymentStatus = request.Model.PaymentStatus;

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
