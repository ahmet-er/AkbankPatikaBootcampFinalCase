using AFC.Schema;
using FluentValidation;

namespace AFC.Business.Validator;

public class ExpenseRequestValidator : AbstractValidator<ExpenseRequestRequest>
{
    public ExpenseRequestValidator()
    {
        
    }
}
