using FluentValidation;

namespace AFC.Data.Helpers;

public class EnumValidator<TEnum> : AbstractValidator<TEnum>
{
    public EnumValidator()
    {
        RuleFor(x => x)
            .Must(BeAValidEnumValue)
            .WithMessage($"{nameof(TEnum)} is not a valid enum value");
    }

    private static bool BeAValidEnumValue(TEnum value)
    {
        if (value is null)
            return false;

        return Enum.IsDefined(typeof(TEnum), value);
    }
}
