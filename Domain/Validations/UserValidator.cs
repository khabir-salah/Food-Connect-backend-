using Domain.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Validations
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(user => user.Email)
                .NotEmpty()
                .WithMessage("Email address is required")
                .EmailAddress()
                .WithMessage("Enter a valid email address");

            RuleFor(user => user.Password)
                .NotEmpty()
                .WithMessage("Password is Required")
                .MinimumLength(4)
                .WithMessage("Password must be at least 5 characters long.");
        }
    }
}
