using AFC.Base.Enums;
using AFC.Base.Response;
using AFC.Schema;
using MediatR;

namespace AFC.Business.Cqrs;

public record CreateUserCommand(UserRequest Model) : IRequest<ApiResponse<UserResponse>>;
public record UpdateUserCommand(int Id, UserRequest Model) : IRequest<ApiResponse>;
public record DeleteUserCommand(int Id) : IRequest<ApiResponse>;

public record GetAllUserQuery() : IRequest<ApiResponse<List<UserResponse>>>;
public record GetUserById(int Id) : IRequest<ApiResponse<UserResponse>>;
public record GetUserByParameter(string UserName, string FirstName, string LastName, string Email, Role Role, DateTime MinLastActivityDate, DateTime MaxLastActivityDate, int Status) : IRequest<ApiResponse<List<UserResponse>>>;