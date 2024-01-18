using AFC.Business.Helpers;
using AFC.Schema;
using FluentValidation;

namespace AFC.Business.Validator;

public class FieldStaffValidator : AbstractValidator<FieldStaffRequest>
{
    public FieldStaffValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("User ID cannot be empty.");

        RuleFor(x => x.IBAN)
            .NotEmpty().WithMessage("IBAN cannot be empty.")
            .Length(26).WithMessage("IBAN must be exactly 26 characters long.")
            .SetValidator(new TurkeyIBANValidator()).WithMessage("Invalid Turkish IBAN.");
    }
}
