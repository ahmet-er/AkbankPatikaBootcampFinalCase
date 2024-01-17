using AFC.Base.Response;
using AFC.Business.Cqrs;
using AFC.Data;
using AFC.Data.Entity;
using AFC.Schema;
using AutoMapper;
using MediatR;
using System.Data.Entity;

namespace AFC.Business.Command;

public class UserCommandHandler :
    IRequestHandler<CreateUserCommand, ApiResponse<UserResponse>>,
    IRequestHandler<UpdateUserCommand, ApiResponse>,
    IRequestHandler<DeleteUserCommand, ApiResponse>
{
    private readonly AfcDbContext dbContext;
    private readonly IMapper mapper;

    public UserCommandHandler(AfcDbContext dbContext, IMapper mapper)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }

    public async Task<ApiResponse<UserResponse>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var checkIdentity = await dbContext.Set<User>().Where(x => x.UserName == request.Model.UserName || x.Email == request.Model.Email).FirstOrDefaultAsync(cancellationToken);
        if (checkIdentity is not null)
            return new ApiResponse<UserResponse>("UserName or Email is in use.");

        var entity = mapper.Map<UserRequest, User>(request.Model);

        var entityResult = await dbContext.AddAsync(entity, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);

        var mapped = mapper.Map<User, UserResponse>(entityResult.Entity);
        return new ApiResponse<UserResponse>(mapped);
    }

    public async Task<ApiResponse> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var fromdb = await dbContext.Set<User>().Where(x => x.Id == request.Id).FirstOrDefaultAsync(cancellationToken);
        if (fromdb is null)
            return new ApiResponse("Record not found.");

        fromdb.FirstName = request.Model.FirstName;
        fromdb.LastName = request.Model.LastName;
        fromdb.Email = request.Model.Email;
        fromdb.Role = request.Model.Role;

        await dbContext.SaveChangesAsync(cancellationToken);
        return new ApiResponse();
    }

    public async Task<ApiResponse> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var fromdb = await dbContext.Set<User>().Where(x => x.Id == request.Id).FirstOrDefaultAsync(cancellationToken);
        if (fromdb is null)
            return new ApiResponse("Record not found.");

        fromdb.IsActive = false;
        await dbContext.SaveChangesAsync(cancellationToken);
        return new ApiResponse();
    }
}
