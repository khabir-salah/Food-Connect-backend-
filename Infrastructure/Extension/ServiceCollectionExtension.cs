using Microsoft.Extensions.DependencyInjection;
using Infrastructure.persistence.Repository.Implementation;
using Application.Features.Command.Create;
using Application.Features.Queries.GeneralServices;
using Application.Features.Queries.Get;
using Application.Features.Interfaces.IRepositries;
using Application.Features.Interfaces.IServices;
using Microsoft.AspNetCore.Http;
using Application.SignalR;


namespace Infrastructure.Extension
{
    public static class ServiceCollectionExtension
    {

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            return services.AddScoped<IDonationRepository, DonationRepository>()
                .AddScoped<IFamilyHeadRepository, FamilyHeadRepository>()
            .AddScoped<IFoodCollectionRepository, FoodCollectionRepository>()
                .AddScoped<IManagerRepository, ManagerRepository>()
                .AddScoped<IOragnisationRepository, OrganisationRepository>()
                .AddScoped<IIndividualRepository, IndividualRepository>()
                .AddScoped<IRoleRepository, RoleRepository>()
                .AddScoped<IUserRepository, UserRepository>()
                .AddScoped<IMessageRepository, MessageRepository>()
                .AddScoped<IFamilyRepository, FamilyRepository>()
                .AddScoped(typeof(GenericRepository<>));
            ;
        }

        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IAuthentication, Authentication>()
                .AddScoped<ICurrentUser, CurrentUser>()
                .AddScoped<IUserService, UserService>()
                .AddScoped<ILogisticsService, LogisticsService>()
                .AddScoped<ITokenService, TokenService>()
                .AddScoped<IDonationValidation, DonationValidation>()
                .AddScoped<IViewProfile, ViewProfile>()
                .AddScoped<IMessageService, MessageService>()
                .AddScoped<IDonationFilter, DonationFilter>()
                .AddTransient<IEmailService, EmailService>()
                .AddTransient<IDonationReview, DonationReview>()
                .AddTransient<IDonationService, DonationService>()
                .AddScoped(typeof(PaginationHelper))
                .AddSingleton<ConnectionMappingService>()
            ;
            services.AddSignalR();
            services.AddHttpContextAccessor();
            services.AddSingleton<IUriService>(o =>
            {
                var accessor = o.GetRequiredService<IHttpContextAccessor>();
                var request = accessor.HttpContext.Request;
                var uri = string.Concat(request.Scheme, "://", request.Host.ToUriComponent());
                return new UriService(uri);
            });
        }


        public static void AddMediateRs(this IServiceCollection services)
        {
             services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(CreateFamilyHead).Assembly));
          

        }


    }
}
