using AFC.Schema;
using FluentValidation;

namespace AFC.Business.Validator;

public class ExpenseRequestByFieldStaffRequestValidator : AbstractValidator<ExpenseRequestByFieldStaffRequest>
{
    public ExpenseRequestByFieldStaffRequestValidator()
    {
        
    }
}

public class ExpenseRequestByAdminRequestValidator : AbstractValidator<ExpenseRequestByAdminRequest>
{
    public ExpenseRequestByAdminRequestValidator()
    {

    }
}
