using AFC.Schema;
using FluentValidation;

namespace AFC.Business.Validator;

public class TokenValidator : AbstractValidator<TokenRequest>
{
    public TokenValidator()
    {
        
    }
}
