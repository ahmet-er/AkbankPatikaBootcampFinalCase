using AFC.Schema;
using FluentValidation;

namespace AFC.Business.Validator;

public class PaymentCategoryValidator : AbstractValidator<PaymentCategoryRequest>
{
    public PaymentCategoryValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name cannot be empty.")
            .MinimumLength(3).WithMessage("Name must be at least 3 characters long.")
            .MaximumLength(128).WithMessage("Name cannot exceed 128 characters.");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Description cannot be empty.")
            .MinimumLength(3).WithMessage("Description must be at least 3 characters long.")
            .MaximumLength(512).WithMessage("Description cannot exceed 512 characters.");
    }
}

public class UpdatePaymentCategoryValidator : AbstractValidator<UpdatePaymentCategoryRequest>
{
    public UpdatePaymentCategoryValidator()
    {
        RuleFor(x => x.Name)
            .MinimumLength(3).WithMessage("Name must be at least 3 characters long.")
            .MaximumLength(128).WithMessage("Name cannot exceed 128 characters.");

        RuleFor(x => x.Description)
            .MinimumLength(3).WithMessage("Description must be at least 3 characters long.")
            .MaximumLength(512).WithMessage("Description cannot exceed 512 characters.");
    }
}
