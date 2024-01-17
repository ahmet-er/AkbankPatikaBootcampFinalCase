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

public class PaymentCategoryCommandHandler :
    IRequestHandler<CreatePaymentCategoryCommand, ApiResponse<PaymentCategoryResponse>>,
    IRequestHandler<UpdatePaymentCategoryCommand, ApiResponse>,
    IRequestHandler<DeletePaymentCategoryCommand, ApiResponse>
{
    private readonly AfcDbContext dbContext;
    private readonly IMapper mapper;
    private readonly IHttpContextAccessor httpContextAccessor;

    public PaymentCategoryCommandHandler(AfcDbContext dbContext, IMapper mapper, IHttpContextAccessor httpContextAccessor)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
        this.httpContextAccessor = httpContextAccessor;
    }

    public async Task<ApiResponse<PaymentCategoryResponse>> Handle(CreatePaymentCategoryCommand request, CancellationToken cancellationToken)
    {
        var checkCategoryName = await dbContext.Set<PaymentCategory>()
            .Where(x => x.Name.ToUpper() == request.Model.Name.ToUpper())
            .FirstOrDefaultAsync(cancellationToken);

        if (checkCategoryName is not null)
            return new ApiResponse<PaymentCategoryResponse>($"{request.Model.Name} is used.");

        var entity = mapper.Map<PaymentCategoryRequest, PaymentCategory>(request.Model);

        BaseEntitySetPropertyExtension.SetCreatedProperties(entity, httpContextAccessor);

        var entityResult = await dbContext.AddAsync(entity, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);

        var mapped = mapper.Map<PaymentCategory, PaymentCategoryResponse>(entityResult.Entity);
        return new ApiResponse<PaymentCategoryResponse>(mapped);
    }

    public async Task<ApiResponse> Handle(UpdatePaymentCategoryCommand request, CancellationToken cancellationToken)
    {
        var fromdb = await dbContext.Set<PaymentCategory>()
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (fromdb is null)
            return new ApiResponse("Record not found.");

        fromdb.Name = request.Model.Name;
        fromdb.Description = request.Model.Description;

        BaseEntitySetPropertyExtension.SetModifiedProperties(fromdb, httpContextAccessor);

        await dbContext.SaveChangesAsync(cancellationToken);
        return new ApiResponse();
    }

    public async Task<ApiResponse> Handle(DeletePaymentCategoryCommand request, CancellationToken cancellationToken)
    {
        var fromdb = await dbContext.Set<PaymentCategory>()
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (fromdb is null)
            return new ApiResponse("Record not found.");

        fromdb.IsActive = false;
        await dbContext.SaveChangesAsync(cancellationToken);
        return new ApiResponse();
    }
}
