using FluentValidation;

namespace AFC.Business.Helpers;

public class TurkeyIBANValidator : AbstractValidator<string>
{
    public TurkeyIBANValidator()
    {
        RuleFor(x => x)
            .Must(BeAValidTurkeyIBAN).WithMessage("Invalid Turkish IBAN.");
    }

    /// <summary>
    /// IBAN'ın Türkiye standatlarına uygunluğunu kontrol eder.
    /// </summary>
    private static bool BeAValidTurkeyIBAN(string value)
    {
        if (string.IsNullOrEmpty(value) || value.Length is not 26 || !value.StartsWith("TR"))
            return false;

        var numericPart = value.Substring(4) + "TR00";
        var numericValue = 0;

        foreach (char c in numericPart)
        {
            if (char.IsDigit(c))
                numericValue = (numericValue * 10 + (c - '0')) % 97;
            else
                numericValue = (numericValue * 100 + (c - 'A' + 10)) % 97;
        }

        return numericValue == 1;
    }
}
