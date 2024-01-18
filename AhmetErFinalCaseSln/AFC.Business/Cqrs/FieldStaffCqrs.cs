using AFC.Base.Response;
using AFC.Schema;
using MediatR;

namespace AFC.Business.Cqrs;

#region Commands
public record CreateFieldStaffCommand(FieldStaffRequest Model) : IRequest<ApiResponse<FieldStaffResponse>>;
public record UpdateFieldStaffCommand(int Id, FieldStaffRequest Model) : IRequest<ApiResponse>;
public record DeleteFieldStaffCommand(int Id) : IRequest<ApiResponse>;

#endregion
#region Queries
public record GetAllFieldStaffQuery() : IRequest<ApiResponse<List<FieldStaffResponse>>>;
public record GetFieldStaffByIdQuery(int Id) : IRequest<ApiResponse<FieldStaffResponse>>;
public record GetFieldStaffByParameterQuery(int? UserId, string IBAN) : IRequest<ApiResponse<List<FieldStaffResponse>>>; 
#endregion