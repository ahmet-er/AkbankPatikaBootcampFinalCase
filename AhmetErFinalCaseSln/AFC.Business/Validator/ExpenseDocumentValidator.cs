using AFC.Data.Entity;
using FluentValidation;

namespace AFC.Business.Validator;

public class ExpenseDocumentValidator : AbstractValidator<ExpenseDocument>
{
    public ExpenseDocumentValidator()
    {
    }
}
