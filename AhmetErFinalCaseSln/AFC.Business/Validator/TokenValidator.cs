using AFC.Schema;
using FluentValidation;

namespace AFC.Business.Validator;

public class TokenValidator : AbstractValidator<TokenRequest>
{
    public TokenValidator()
    {
        RuleFor(x => x.Username)
            .NotEmpty().WithMessage("Username cannot be empty.")
            .MinimumLength(5).WithMessage("Username must be at least 5 characters long.")
            .MaximumLength(256).WithMessage("Username cannot exceed 256 characters.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password cannot be empty.")
            .MinimumLength(5).WithMessage("Password must be at least 5 characters long.")
            .MaximumLength(256).WithMessage("Password cannot exceed 256 characters.");
    }
}
