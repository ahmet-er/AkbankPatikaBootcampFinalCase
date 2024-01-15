using AFC.Base.Response;
using AFC.Business.Cqrs;
using AFC.Schema;
using MediatR;

namespace AFC.Business.Query;

public class FieldStaffQueryHandler :
    IRequestHandler<GetAllFieldStaffQuery, ApiResponse<List<FieldStaffResponse>>>,
    IRequestHandler<GetFieldStaffByIdQuery, ApiResponse<FieldStaffResponse>>,
    IRequestHandler<GetFieldStaffByParameterQuery, ApiResponse<List<FieldStaffResponse>>>
{
    public Task<ApiResponse<List<FieldStaffResponse>>> Handle(GetAllFieldStaffQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<ApiResponse<FieldStaffResponse>> Handle(GetFieldStaffByIdQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<ApiResponse<List<FieldStaffResponse>>> Handle(GetFieldStaffByParameterQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
