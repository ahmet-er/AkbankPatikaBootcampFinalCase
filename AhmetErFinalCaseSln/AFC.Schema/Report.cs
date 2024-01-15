using AFC.Base.Enums;
using AFC.Base.Schema;

namespace AFC.Schema;

public class ReportRequest : BaseRequest
{
    public string Name { get; set; }
    public ReportType ReportType { get; set; }
    public ReportPeriod ReportPeriod { get; set; }
}

public class ReportResponse : BaseResponse
{
    public string Name { get; set; }
    public ReportType ReportType { get; set; }
    public ReportPeriod ReportPeriod { get; set; }
}