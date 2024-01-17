using AFC.Base.Response;
using AFC.Business.Cqrs;
using AFC.Business.Helpers;
using AFC.Data;
using AFC.Data.Entity;
using AFC.Schema;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace AFC.Business.Command;

public class FieldStaffCommandHandler :
    IRequestHandler<CreateFieldStaffCommand, ApiResponse<FieldStaffResponse>>,
    IRequestHandler<UpdateFieldStaffCommand, ApiResponse>,
    IRequestHandler<DeleteFieldStaffCommand, ApiResponse>
{
    private readonly AfcDbContext dbContext;
    private readonly IMapper mapper;
    private readonly IHttpContextAccessor httpContextAccessor;

    public FieldStaffCommandHandler(AfcDbContext dbContext, IMapper mapper, IHttpContextAccessor httpContextAccessor)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
        this.httpContextAccessor = httpContextAccessor;
    }
    public async Task<ApiResponse<FieldStaffResponse>> Handle(CreateFieldStaffCommand request, CancellationToken cancellationToken)
    {
        var checkIBAN = await dbContext.Set<FieldStaff>()
            .FirstOrDefaultAsync(x => x.IBAN == request.Model.IBAN, cancellationToken);

        if (checkIBAN is not null)
            return new ApiResponse<FieldStaffResponse>($"{request.Model.IBAN} is used by another field staff.");

        var entity = mapper.Map<FieldStaffRequest, FieldStaff>(request.Model);

        BaseEntitySetPropertyExtension.SetCreatedProperties(entity, httpContextAccessor);

        var entityResult = await dbContext.AddAsync(entity, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);

        var mapped = mapper.Map<FieldStaff, FieldStaffResponse>(entityResult.Entity);
        return new ApiResponse<FieldStaffResponse>(mapped);
    }

    public async Task<ApiResponse> Handle(UpdateFieldStaffCommand request, CancellationToken cancellationToken)
    {
        var fromdb = await dbContext.Set<FieldStaff>()
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (fromdb is null)
            return new ApiResponse("Record not found.");

        fromdb.IBAN = request.Model.IBAN;

        BaseEntitySetPropertyExtension.SetModifiedProperties(fromdb, httpContextAccessor);

        await dbContext.SaveChangesAsync(cancellationToken);
        return new ApiResponse();
    }

    public async Task<ApiResponse> Handle(DeleteFieldStaffCommand request, CancellationToken cancellationToken)
    {
        var fromdb = await dbContext.Set<FieldStaff>()
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (fromdb is null) 
            return new ApiResponse("Record not found.");

        fromdb.IsActive = false;
        await dbContext.SaveChangesAsync(cancellationToken);
        return new ApiResponse();
    }
}
