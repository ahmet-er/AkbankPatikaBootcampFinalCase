using AFC.Base.Response;
using AFC.Schema;
using MediatR;

namespace AFC.Business.Cqrs;

#region Commands
public record CreateUserCommand(UserRequest Model) : IRequest<ApiResponse<UserResponse>>;
public record UpdateUserCommand(int Id, UserRequest Model) : IRequest<ApiResponse>;
public record DeleteUserCommand(int Id) : IRequest<ApiResponse>; 
#endregion

#region Queries
public record GetAllUserQuery() : IRequest<ApiResponse<List<UserResponse>>>;
public record GetUserById(int Id) : IRequest<ApiResponse<UserResponse>>;
public record GetUserByParameter(string UserName, string FirstName, string LastName, string Email, string Role) : IRequest<ApiResponse<List<UserResponse>>>; 
#endregion