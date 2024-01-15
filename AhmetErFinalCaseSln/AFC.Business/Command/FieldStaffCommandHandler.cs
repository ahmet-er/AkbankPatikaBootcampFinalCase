using AFC.Base.Response;
using AFC.Business.Cqrs;
using AFC.Schema;
using MediatR;

namespace AFC.Business.Command;

public class FieldStaffCommandHandler :
    IRequestHandler<CreateFieldStaffCommand, ApiResponse<FieldStaffResponse>>,
    IRequestHandler<UpdateFieldStaffCommand, ApiResponse>,
    IRequestHandler<DeleteFieldStaffCommand, ApiResponse>
{
    public Task<ApiResponse<FieldStaffResponse>> Handle(CreateFieldStaffCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<ApiResponse> Handle(UpdateFieldStaffCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<ApiResponse> Handle(DeleteFieldStaffCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
