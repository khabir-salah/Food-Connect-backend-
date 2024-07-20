using Domain.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Validations
{
    public class OrganisationValidation : AbstractValidator<Organisation>
    {
        public OrganisationValidation() 
        {
            RuleFor(organization => organization.OganisationName)
               .NotEmpty()
               .WithMessage("First Name is required");

            RuleFor(organization => organization.CacNumber)
                .NotEmpty()
                .WithMessage("Last Name is required");

            RuleFor(family => family.City)
                .NotEmpty()
                .WithMessage("City is required");

            RuleFor(organization => organization.Capacity)
                .NotEmpty()
                .InclusiveBetween(5, 200)
                .WithMessage("size of organization must be between 5 - 200");

            RuleFor(organization => organization.PhoneNumber)
                .NotEmpty()
                .Matches(@"^\d{10}$")
                .WithMessage("Invalid phone number.");

            RuleFor(organization => organization.Address)
                .NotEmpty()
                .WithMessage("Enter a valid address");

            RuleFor(organization => organization.LOcalGovernment)
                .NotEmpty()
                .WithMessage("Enter a valid Local government");

            RuleFor(organization => organization.PostalCode)
            .NotEmpty()
            .WithMessage("Postal code is required.")
            .Matches(@"^\d{6}$")
            .WithMessage("Enter a valid postal code.");

            RuleFor(organization => organization.CacNumber)
            .NotEmpty()
            .WithMessage("CAC number is required.")
            .Matches(@"^[A-Z]{2}\d{6}$")
            .WithMessage("Enter a valid Nigerian CAC number.");
        }
    }
}
