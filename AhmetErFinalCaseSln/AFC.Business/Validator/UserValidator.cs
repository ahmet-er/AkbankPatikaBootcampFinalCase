﻿using AFC.Schema;
using FluentValidation;

namespace AFC.Business.Validator;

public class UserValidator : AbstractValidator<UserRequest>
{
    public UserValidator()
    {
        RuleFor(x => x.UserName)
            .NotEmpty().WithMessage("UserName cannot be empty.")
            .MinimumLength(5).WithMessage("UserName must be at least 5 characters long.")
            .MaximumLength(256).WithMessage("UserName cannot exceed 256 characters.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password cannot be empty.")
            .MinimumLength(5).WithMessage("Password must be at least 5 characters long.")
            .MaximumLength(256).WithMessage("Password cannot exceed 256 characters.");

        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("FirstName cannot be empty.")
            .MinimumLength(2).WithMessage("FirstName must be at least 2 characters long.")
            .MaximumLength(128).WithMessage("FirstName cannot exceed 128 characters.");

        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("LastName cannot be empty.")
            .MinimumLength(2).WithMessage("LastName must be at least 2 characters long.")
            .MaximumLength(128).WithMessage("LastName cannot exceed 128 characters.");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email cannot be empty.")
            .MaximumLength(256).WithMessage("Email cannot exceed 256 characters.")
            .EmailAddress().WithMessage("Invalid email address");

        RuleFor(x => x.Role)
            .NotEmpty().WithMessage("Role cannot be empty.")
            .IsInEnum().WithMessage("Invalid Role value.");
    }
}

public class UpdateUserValidator : AbstractValidator<UpdateUserRequest>
{
    public UpdateUserValidator()
    {
        RuleFor(x => x.UserName)
            .MinimumLength(5).WithMessage("UserName must be at least 5 characters long.")
            .MaximumLength(256).WithMessage("UserName cannot exceed 256 characters.");

        RuleFor(x => x.FirstName)
            .MinimumLength(5).WithMessage("FirstName must be at least 5 characters long.")
            .MaximumLength(128).WithMessage("FirstName cannot exceed 128 characters.");

        RuleFor(x => x.LastName)
            .MinimumLength(5).WithMessage("LastName must be at least 5 characters long.")
            .MaximumLength(128).WithMessage("LastName cannot exceed 128 characters.");

        RuleFor(x => x.Email)
            .MaximumLength(256).WithMessage("Email cannot exceed 256 characters.")
            .EmailAddress().WithMessage("Invalid email address");
    }
}
