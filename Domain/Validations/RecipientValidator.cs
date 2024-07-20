using Domain.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Validations
{
    public class RecipientValidator : AbstractValidator<Recipent>
    {
        public RecipientValidator() 
        {
            RuleFor(recipent => recipent.FirstName)
                .NotEmpty()
                .WithMessage("First Name is required");

            RuleFor(recipent => recipent.LastName)
                .NotEmpty()
                .WithMessage("Last Name is required");
            RuleFor(recipent => recipent.City)
                .NotEmpty()
                .WithMessage("City is required");

            RuleFor(recipent => recipent.PhoneNumber)
                .NotEmpty()
                .Matches(@"^\d{10}$")
                .WithMessage("Invalid phone number.");

            RuleFor(recipent => recipent.Address)
                .NotEmpty()
                .WithMessage("Enter a valid address");

            RuleFor(recipent => recipent.LOcalGovernment)
                .NotEmpty()
                .WithMessage("Enter a valid Local government");

            RuleFor(recipent => recipent  .PostalCode)
            .NotEmpty()
            .WithMessage("Postal code is required.")
            .Matches(@"^\d{6}$")
            .WithMessage("Enter a valid postal code.");
        }
    }
}
