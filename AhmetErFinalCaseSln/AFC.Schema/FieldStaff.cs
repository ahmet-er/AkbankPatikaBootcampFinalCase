using AFC.Base.Schema;

namespace AFC.Schema;

public class FieldStaffRequest : BaseRequest
{
    public int UserId { get; set; }
    public string IBAN { get; set; }
}

public class FieldStaffResponse : BaseResponse
{
    public int UserId { get; set; }
    public string IBAN { get; set; }

    public virtual List<ExpenseRequestRequest> ExpenseRequests { get; set; }
}