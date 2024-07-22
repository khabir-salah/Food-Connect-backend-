using Application.Command;
using Application.Queries;
using Microsoft.Extensions.DependencyInjection;
using Application.Interfaces;
using Infrastructure.persistence.Repository.Implementation;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Infrastructure.persistence.Context;
using Microsoft.AspNet.Identity;


namespace Infrastructure.Extension
{
    public static class ServiceCollectionExtension
    {

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            return services.AddScoped<IDonationRepository, DonationRepository>()
                .AddScoped<IFamilyRepository, FamilyRepository>()
            .AddScoped<IFoodCollectionRepository, FoodCollectionRepository>()
                .AddScoped<IManagerRepository, ManagerRepository>()
                .AddScoped<IOragnisationRepository, OrganisationRepository>()
                .AddScoped<IRecipentRepository, RecipentRepository>()
                .AddScoped<IRoleRepository, RoleRepository>()
                .AddScoped<IUserRepository, UserRepository>()
                .AddScoped<Authentication>()
                .AddScoped(typeof(GenericRepository<>));
            ;
        }


        public static void AddMediateRs(this IServiceCollection services)
        {
             services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(CreateFamily).Assembly));
            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<FoodConnectDB>()
        .AddDefaultTokenProviders();

            services.AddIdentity<User, IdentityRole>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = true;
                options.SignIn.RequireConfirmedEmail = true;
            })
    .AddDefaultTokenProviders();

            // Manually add EF Core stores
            services.AddScoped<Microsoft.AspNet.Identity.IUserStore<User>, UserStore<ApplicationUser, IdentityRole, FoodConnectDB>>();
            services.AddScoped<IRoleStore<IdentityRole>, RoleStore<IdentityRole, FoodConnectDB>>();





            services.AddTransient< EmailService>();

            services.Configure<IdentityOptions>(options =>
            {
                options.SignIn.RequireConfirmedEmail = true;
            });

             services.AddScoped<Authentication>().
                AddScoped<ResetPasswordService>()
                .AddScoped<UserLogin>();



        }


    }
}
