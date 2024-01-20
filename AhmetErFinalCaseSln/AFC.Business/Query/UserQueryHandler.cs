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

public class UserQueryHandler :
    IRequestHandler<GetAllUserQuery, ApiResponse<List<UserResponse>>>,
    IRequestHandler<GetUserByIdQuery, ApiResponse<UserResponse>>,
    IRequestHandler<GetUserByParameterQuery, ApiResponse<List<UserResponse>>>
{
    private readonly AfcDbContext dbContext;
    private readonly IMapper mapper;

    public UserQueryHandler(AfcDbContext dbContext, IMapper mapper)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }

    public async Task<ApiResponse<List<UserResponse>>> Handle(GetAllUserQuery request, CancellationToken cancellationToken)
    {
        var list= await dbContext.Set<User>()
            .Where(x => x.IsActive == true)
            .ToListAsync(cancellationToken);

        var mappedList = mapper.Map<List<User>, List<UserResponse>>(list);
        return new ApiResponse<List<UserResponse>>(mappedList);
    }

    public async Task<ApiResponse<UserResponse>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await dbContext.Set<User>().FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (entity is null || entity.IsActive == false)
            return new ApiResponse<UserResponse>("Record not found.");

        var mapped = mapper.Map<User, UserResponse>(entity);
        return new ApiResponse<UserResponse>(mapped);

    }

    public async Task<ApiResponse<List<UserResponse>>> Handle(GetUserByParameterQuery request, CancellationToken cancellationToken)
    {
        var predicate = PredicateBuilder.New<User>(true);
        if (!string.IsNullOrEmpty(request.UserName))
            predicate.And(x => x.UserName.Contains(request.UserName));
        if (!string.IsNullOrEmpty(request.FirstName))
            predicate.And(x => x.FirstName.Contains(request.FirstName));
        if (!string.IsNullOrEmpty(request.LastName))
            predicate.And(x => x.LastName.Contains(request.LastName));
        if (!string.IsNullOrEmpty(request.Email))
            predicate.And(x => x.Email.Contains(request.Email));

        if (!string.IsNullOrEmpty(request.Role) && Enum.TryParse<Role>(request.Role, true, out var parsedRole))
            predicate.And(x => x.Role == parsedRole);
        else if (!string.IsNullOrEmpty(request.Role))
            return new ApiResponse<List<UserResponse>>("Invalid role type.");

        var list = await dbContext.Set<User>()
            .Where(x => x.IsActive)
            .Where(predicate)
            .ToListAsync(cancellationToken);

        var mappedList = mapper.Map<List<User>, List<UserResponse>>(list);
        return new ApiResponse<List<UserResponse>>(mappedList);
    }
}
