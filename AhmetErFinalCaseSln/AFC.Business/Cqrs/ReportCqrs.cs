using AFC.Base.Enums;
using AFC.Base.Response;
using AFC.Schema;
using MediatR;

namespace AFC.Business.Cqrs;

public record CreateReportCommand(ReportRequest Model) : IRequest<ApiResponse<ReportResponse>>;
public record UpdateReportCommand(int Id, ReportRequest Model) : IRequest<ApiResponse>;
public record DeleteReportCommand(int Id) : IRequest<ApiResponse>;

public record GetAllReportQuery() : IRequest<ApiResponse<List<ReportResponse>>>;
public record GetReportById() : IRequest<ApiResponse<ReportResponse>>;
public record GetReportByParameter(string Name, ReportType ReportType, ReportPeriod ReportPeriod) : IRequest<ApiResponse<List<ReportResponse>>>;