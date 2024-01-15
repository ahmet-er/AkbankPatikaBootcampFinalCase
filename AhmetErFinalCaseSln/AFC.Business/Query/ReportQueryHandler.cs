using AFC.Base.Response;
using AFC.Business.Cqrs;
using AFC.Schema;
using MediatR;

namespace AFC.Business.Query;

public class ReportQueryHandler :
    IRequestHandler<GetAllReportQuery, ApiResponse<List<ReportResponse>>>,
    IRequestHandler<GetReportById, ApiResponse<ReportResponse>>,
    IRequestHandler<GetReportByParameter, ApiResponse<List<ReportResponse>>>
{
    public Task<ApiResponse<List<ReportResponse>>> Handle(GetAllReportQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<ApiResponse<ReportResponse>> Handle(GetReportById request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<ApiResponse<List<ReportResponse>>> Handle(GetReportByParameter request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
