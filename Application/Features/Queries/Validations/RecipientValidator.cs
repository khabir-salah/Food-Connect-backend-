using Application.Features.DTOs;
using Domain.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Validations
{
    public class RecipientValidator : AbstractValidator<CreateRecipientCommandModel.CreateRecipientCommand>
    {
        public RecipientValidator() 
        {
            RuleFor(recipent => recipent.FirstName)
                .NotEmpty()
                .WithMessage("First Name is required");

            RuleFor(recipent => recipent.LastName)
                .NotEmpty()
                .WithMessage("Last Name is required");
            //RuleFor(recipent => recipent.City)
            //    .NotEmpty()
            //    .WithMessage("City is required");

            RuleFor(recipent => recipent.PhoneNumber)
                .NotEmpty()
                .Matches(@"^\d{10}$")
                .WithMessage("Invalid phone number.");

            //RuleFor(recipent => recipent.Address)
            //    .NotEmpty()
            //    .WithMessage("Enter a valid address");

            //RuleFor(recipent => recipent.LOcalGovernment)
            //    .NotEmpty()
            //    .WithMessage("Enter a valid Local government");

            //RuleFor(recipent => recipent  .PostalCode)
            //.NotEmpty()
            //.WithMessage("Postal code is required.")
            //.Matches(@"^\d{6}$")
            //.WithMessage("Enter a valid postal code.");

            RuleFor(recipent => recipent.Nin).NotEmpty().MinimumLength(11);

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
