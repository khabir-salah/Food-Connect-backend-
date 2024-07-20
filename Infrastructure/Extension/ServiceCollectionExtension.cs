using Application.Command;
using Application.Queries;
using Domain.Validations;
using Microsoft.Extensions.DependencyInjection;
using FluentValidation;
using FluentValidation.AspNetCore;
using Application.Interfaces;
using Infrastructure.persistence.Repository.Implementation;
using Domain.Entities;


namespace Infrastructure.Extension
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddCommands(this IServiceCollection services)
        {
            return services.AddMediatR(cfg =>
            cfg
            .RegisterServicesFromAssemblyContaining<UserLogin>());
           
        }

      

        public static IServiceCollection AddValidations(this IServiceCollection services)
        {
            return services.AddFluentValidation(fv =>
            
                fv.RegisterValidatorsFromAssemblyContaining<FamilyValidation>()
               
            );
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            return services.AddScoped<IDonationRepository, DonationRepository>()
                .AddScoped<IFamilyRepository>()
            .AddScoped<IFoodCollectionRepository>()
                .AddScoped<IManagerRepository>()
                .AddScoped<IOragnisationRepository>()
                .AddScoped<IRecipentRepository>()
                .AddScoped<IRoleRepository>()
                .AddScoped<IUserRepository>();
        }
        //.RegisterServicesFromAssemblyContaining<CreateFamily>()
        //    .RegisterServicesFromAssemblyContaining<CreateManager>()
        //    .RegisterServicesFromAssemblyContaining<CreateOrganization>()
        //    .RegisterServicesFromAssemblyContaining<CreateRecipient>()

         //.RegisterValidatorsFromAssemblyContaining<ManagerValidator>()
         //       .RegisterValidatorsFromAssemblyContaining<OrganisationValidation>()
         //       .RegisterValidatorsFromAssemblyContaining<RecipientValidator>()
         //       .RegisterValidatorsFromAssemblyContaining<UserValidator>()
    }
}
