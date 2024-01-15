using AFC.Base.Response;
using AFC.Business.Cqrs;
using AFC.Schema;
using MediatR;

namespace AFC.Business.Command;

public class ReportCommandHandler :
    IRequestHandler<CreateReportCommand, ApiResponse<ReportResponse>>,
    IRequestHandler<UpdateReportCommand, ApiResponse>,
    IRequestHandler<DeleteReportCommand, ApiResponse>
{
    public Task<ApiResponse<ReportResponse>> Handle(CreateReportCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<ApiResponse> Handle(UpdateReportCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<ApiResponse> Handle(DeleteReportCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
