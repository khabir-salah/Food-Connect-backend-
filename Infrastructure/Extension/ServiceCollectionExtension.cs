using Application.Command;
using Application.Queries;
using Infrastructure.persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Extension
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddCommands(this IServiceCollection services)
        {
            return services.AddScoped<CreateFamily>()
                .AddScoped<CreateManager>()
                .AddScoped<CreateOrganization>()
                .AddScoped<CreateRecipient>();
        }

        public static IServiceCollection AddQueries(this IServiceCollection services)
        {
            return services.AddScoped<UserLogin>();
        }
    }
}
