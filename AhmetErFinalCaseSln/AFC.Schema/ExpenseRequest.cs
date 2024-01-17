using AFC.Base.Enums;
using AFC.Base.Schema;

namespace AFC.Schema;

public class ExpenseRequestRequest : BaseRequest
{
    public int FieldStaffId { get; set; }
    public int PaymentCategoryId { get; set; }
    public decimal Amount { get; set; }
    public string Description { get; set; }
    public string CompanyResultDescription { get; set; }
    public string PaymentLocation { get; set; }
    public ExpenseStatus ExpenseStatus { get; set; }
    public PaymentStatus PaymentStatus { get; set; }
}

public class ExpenseRequestResponse : BaseResponse
{
    public int FieldStaffId { get; set; }
    public int PaymentCategoryId { get; set; }
    public decimal Amount { get; set; }
    public string Description { get; set; }
    public string CompanyResultDescription { get; set; }
    public string PaymentLocation { get; set; }
    public ExpenseStatus ExpenseStatus { get; set; }
    public PaymentStatus PaymentStatus { get; set; }
}