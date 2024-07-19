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
        public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {

            return services.AddDbContext<FoodConnectDB>(options => options.UseMySQL(configuration.GetConnectionString("FoodConnectConnection")));

            
        }
    }
}
