using AFC.Base.Schema;

namespace AFC.Schema;

public class ExpenseReportResponse: BaseResponse
{
    public int FieldStaffId { get; set; }
    public virtual FieldStaffResponse FieldStaff { get; set; }
    public int PaymentCategoryId { get; set; }
    public virtual PaymentCategoryResponse PaymentCategory { get; set; }
    public decimal Amount { get; set; }
    public string Description { get; set; }
    public string CompanyResultDescription { get; set; }
    public string PaymentLocation { get; set; }
    public string ExpenseStatus { get; set; }
    public string PaymentStatus { get; set; }
    public virtual List<ExpenseDocumentResponse> ExpenseDocuments { get; set; }
}