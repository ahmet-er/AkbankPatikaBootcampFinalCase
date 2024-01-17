using AFC.Base.Response;
using AFC.Business.Cqrs;
using AFC.Data;
using AFC.Data.Entity;
using AFC.Schema;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AFC.Business.Command;

public class FieldStaffCommandHandler :
    IRequestHandler<CreateFieldStaffCommand, ApiResponse<FieldStaffResponse>>,
    IRequestHandler<UpdateFieldStaffCommand, ApiResponse>,
    IRequestHandler<DeleteFieldStaffCommand, ApiResponse>
{
    private readonly AfcDbContext dbContext;
    private readonly IMapper mapper;
    private readonly IMediator mediator;

    public FieldStaffCommandHandler(AfcDbContext dbContext, IMapper mapper, IMediator mediator)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
        this.mediator = mediator;
    }
    public async Task<ApiResponse<FieldStaffResponse>> Handle(CreateFieldStaffCommand request, CancellationToken cancellationToken)
    {
        var operation = new CreateUserCommand(request.Model.User);
        var result = await mediator.Send(operation);
        var user = await dbContext.Set<User>()
            .FirstOrDefaultAsync(x => x.UserName == result.Response.UserName, cancellationToken);

        if (user is not null)
            request.Model.UserId = user.Id;

        var checkIBAN = await dbContext.Set<FieldStaff>()
            .FirstOrDefaultAsync(x => x.IBAN == request.Model.IBAN, cancellationToken);

        if (checkIBAN is not null)
            return new ApiResponse<FieldStaffResponse>($"{request.Model.IBAN} is used by another field staff.");

        var entity = mapper.Map<FieldStaffRequest, FieldStaff>(request.Model);

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
