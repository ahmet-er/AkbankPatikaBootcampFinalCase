using AFC.Schema;
using FluentValidation;

namespace AFC.Business.Validator;

public class UserValidator : AbstractValidator<UserRequest>
{
    public UserValidator()
    {

    }
}
