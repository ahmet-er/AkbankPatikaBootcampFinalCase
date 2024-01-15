using AFC.Schema;
using FluentValidation;

namespace AFC.Business.Validator;

public class PaymentCategoryValidator : AbstractValidator<PaymentCategoryRequest>
{
    public PaymentCategoryValidator()
    {
        
    }
}
