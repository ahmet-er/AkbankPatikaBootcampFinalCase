using AFC.Schema;
using FluentValidation;

namespace AFC.Business.Validator;

public class ExpenseRequestByFieldStaffRequestValidator : AbstractValidator<ExpenseRequestByFieldStaffRequest>
{
    public ExpenseRequestByFieldStaffRequestValidator()
    {
        RuleFor(x => x.FieldStaffId)
            .NotEmpty().WithMessage("FieldStaffId cannot be empty.");

        RuleFor(x => x.PaymentCategoryId)
            .NotEmpty().WithMessage("PaymentCategoryId cannot be empty.");

        RuleFor(x => x.Amount)
            .NotEmpty().WithMessage("Amount cannot be empty.")
            .GreaterThan(0).WithMessage("Amount must be greater than zero.");

        RuleFor(x => x.Description)
            .MaximumLength(512).WithMessage("Description cannot be empty.");

        RuleFor(x => x.PaymentLocation)
            .NotNull().NotEmpty().WithMessage("PaymentLocation cannot be empty.")
            .MaximumLength(512).WithMessage("PaymentLocation cannot exceed 512 characters.");
    }
}

public class UpdateExpenseRequestByFieldStaffRequestValidator : AbstractValidator<UpdateExpenseRequestByFieldStaffRequest>
{
    public UpdateExpenseRequestByFieldStaffRequestValidator()
    {
        RuleFor(x => x.Amount)
            .GreaterThan(0).WithMessage("Amount must be greater than zero.");

        RuleFor(x => x.Description)
            .MaximumLength(512).WithMessage("Description cannot be empty.");

        RuleFor(x => x.PaymentLocation)
            .MaximumLength(512).WithMessage("PaymentLocation cannot exceed 512 characters.");
    }
}

public class ExpenseRequestByAdminRequestValidator : AbstractValidator<UpdateExpenseRequestByAdminRequest>
{
    public ExpenseRequestByAdminRequestValidator()
    {
        RuleFor(x => x.CompanyResultDescription)
            .MaximumLength(512).WithMessage("Description cannot be empty.");

        RuleFor(x => x.ExpenseStatus)
            .IsInEnum().WithMessage("Invalid ExpenseStatus value.");
    }
}
