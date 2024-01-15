using AFC.Base.Response;
using AFC.Business.Cqrs;
using AFC.Schema;
using MediatR;

namespace AFC.Business.Query;

public class UserQueryHandler :
    IRequestHandler<GetAllUserQuery, ApiResponse<List<UserResponse>>>,
    IRequestHandler<GetUserById, ApiResponse<UserResponse>>,
    IRequestHandler<GetUserByParameter, ApiResponse<List<UserResponse>>>
{
    public Task<ApiResponse<List<UserResponse>>> Handle(GetAllUserQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<ApiResponse<UserResponse>> Handle(GetUserById request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<ApiResponse<List<UserResponse>>> Handle(GetUserByParameter request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
