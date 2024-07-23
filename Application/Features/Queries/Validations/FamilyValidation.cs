using Application.Features.DTOs;
using FluentValidation;


namespace Domain.Validations
{
    public class FamilyValidation : AbstractValidator<CreateFamilyCommandModel.CreateFamilyCommand>
    {
        public FamilyValidation()
        {
            RuleFor(family => family.FirstName)
                .NotEmpty()
                .WithMessage("First Name is required");

            RuleFor(family => family.LastName)
                .NotEmpty()
                .WithMessage("Last Name is required");
            //RuleFor(family => family.City)
            //    .NotEmpty()
            //    .WithMessage("City is required");

            RuleFor(family => family.FamilyCount)
                .NotEmpty()
                .InclusiveBetween(4, 12)
                .WithMessage("Number of Family must be between 4 - 12");

            RuleFor(family => family.PhoneNumber)
                .NotEmpty()
                .Matches(@"^\d{10}$")
                .WithMessage("Invalid phone number.");

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

            //RuleFor(family => family.Address)
            //    .NotEmpty()
            //    .WithMessage("Enter a valid address");

            //RuleFor(family => family.LOcalGovernment)
            //    .NotEmpty()
            //    .WithMessage("Enter a valid Local government");

            //RuleFor(family => family.PostalCode)
            //.NotEmpty()
            //.WithMessage("Postal code is required.")
            //.Matches(@"^\d{6}$")
            //.WithMessage("Enter a valid postal code.");

        }

    }
}
