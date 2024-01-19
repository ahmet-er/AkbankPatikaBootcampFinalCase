namespace AFC.Schema;

public class ExpensePaymentRequest
{
    public int ExpenseRequestId { get; set; }
    public string CurrencyType { get; set; }
}

public class ExpensePaymentResponse
{
    public int ExpenseRequestId { get; set; }
    public ExpenseRequestResponse ExpenseRequest { get; set; }
    public string TransferType { get; set; }
    public string PaymentDescription { get; set; }
}