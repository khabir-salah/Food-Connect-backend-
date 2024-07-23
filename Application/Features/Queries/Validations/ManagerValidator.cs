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
    public class ManagerValidator : AbstractValidator<CreateManagerCommandModel.CreateManagerCommand>
    {
        public ManagerValidator() 
        {
            RuleFor(Manager => Manager.FirstName)
               .NotEmpty()
               .WithMessage("First Name is required");

            RuleFor(Manager => Manager.LastName)
                .NotEmpty()
                .WithMessage("Last Name is required");

            //RuleFor(Manager => Manager.City)
            //    .NotEmpty()
            //    .WithMessage("City is required");

            //RuleFor(Manager => Manager.PhoneNumber)
            //    .NotEmpty()
            //    .Matches(@"^\d{10}$")
            //    .WithMessage("Invalid phone number.");

            //RuleFor(Manager => Manager.Address)
            //    .NotEmpty()
            //    .WithMessage("Enter a valid address");

            //RuleFor(Manager => Manager.LOcalGovernment)
            //    .NotEmpty()
            //    .WithMessage("Enter a valid Local government");

            //RuleFor(Manager => Manager .PostalCode)
            //.NotEmpty()
            //.WithMessage("Postal code is required.")
            //.Matches(@"^\d{6}$")
            //.WithMessage("Enter a valid postal code.");

            //RuleFor(Manager => Manager.Nin).NotEmpty().MinimumLength(11);

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
