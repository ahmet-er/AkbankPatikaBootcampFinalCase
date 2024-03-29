﻿using AFC.Schema;
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
            .Custom((value, context) =>
            {
                var cleanIBAN = value?.Replace(" ", "");

                if (!IBANValidator.BeAValidTurkeyIBAN(cleanIBAN))
                    context.AddFailure("Invalid Turkish IBAN");
            });
    }
}
public class UpdateFieldStaffValidator : AbstractValidator<UpdateFieldStaffRequest>
{
    public UpdateFieldStaffValidator()
    {
        RuleFor(x => x.IBAN)
            .NotEmpty().WithMessage("IBAN cannot be empty.")
            .Custom((value, context) =>
            {
                var cleanIBAN = value?.Replace(" ", "");

                if (!IBANValidator.BeAValidTurkeyIBAN(cleanIBAN))
                    context.AddFailure("Invalid Turkish IBAN");
            });
    }
}

public static class IBANValidator
{
    /// <summary>
    /// IBAN'ın Türkiye standatlarına uygunluğunu kontrol eder.
    /// </summary>
    public static bool BeAValidTurkeyIBAN(string value)
    {
        if (string.IsNullOrEmpty(value) || value.Length is not 26 || !value.StartsWith("TR"))
            return false;

        return value.Skip(2).All(char.IsDigit);
    }
}
