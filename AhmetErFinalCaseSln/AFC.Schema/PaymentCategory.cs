﻿using AFC.Base.Schema;

namespace AFC.Schema;

public class PaymentCategoryRequest : BaseRequest
{
    public string Name { get; set; }
    public string Description { get; set; }
}

public class PaymentCategoryResponse : BaseResponse
{
    public string Name { get; set; }
    public string Description { get; set; }

    public virtual List<ExpenseRequestRequest> ExpenseRequests { get; set; }
}