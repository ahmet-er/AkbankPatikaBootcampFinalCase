using AFC.Schema;
using FluentValidation;

namespace AFC.Business.Validator;

public class ExpenseDocumentValidator : AbstractValidator<ExpenseDocumentRequest>
{
    public ExpenseDocumentValidator()
    {
        RuleFor(x => x.ExpenseRequestId)
            .NotEmpty().WithMessage("Expense Request ID cannot be empty.");

        RuleFor(x => x.FormFile)
            .NotEmpty().WithMessage("File cannot be empty.")
            .Must(file => file is not null && file.Length > 0 && file.Length <= 10 * 1024 * 1024)
            .WithMessage("File size must be between 1 byte and 10 MB.");
    }
}

public class UpdateExpenseDocumentValidator : AbstractValidator<UpdateExpenseDocumentRequest>
{
    public UpdateExpenseDocumentValidator()
    {
        RuleFor(x => x.FormFile)
            .Must(file => file is not null && file.Length > 0 && file.Length <= 10 * 1024 * 1024)
            .WithMessage("File size must be between 1 byte and 10 MB.");
    }
}
