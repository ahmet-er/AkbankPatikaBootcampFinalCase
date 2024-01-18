using AFC.Base.Enums;
using AFC.Data.Helpers;
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
            .MaximumLength(512).WithMessage("");
    }
}

public class ExpenseRequestByAdminRequestValidator : AbstractValidator<ExpenseRequestByAdminRequest>
{
    public ExpenseRequestByAdminRequestValidator()
    {
        RuleFor(x => x.CompanyResultDescription)
            .MaximumLength(512).WithMessage("Company result description cannot be empty.");

        RuleFor(x => x.ExpenseStatus)
            .NotEmpty().WithMessage("ExpenseStatus cannot be empty.")
            .IsInEnum().WithMessage("Invalid ExpenseStatus value.");

        RuleFor(x => x.PaymentStatus)
            .NotEmpty().WithMessage("PaymentStatus cannot be empty.")
            .IsInEnum().WithMessage("Invalid PaymentStatus value.");
    }
}
