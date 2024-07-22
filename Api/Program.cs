
using Application.Queries;
using Asp.Versioning;
using Domain.Validations;
using FluentValidation;
using FluentValidation.AspNetCore;
using Infrastructure.Extension;
using Infrastructure.persistence.Context;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Logging.ClearProviders().AddConsole();
            var connectionString = builder.Configuration.GetConnectionString("FoodConnectConnection");
            // Add services to the container.
            builder.Services.AddRepositories();
            builder.Services.AddMediateR();
            builder.Services.AddFluentValidationAutoValidation();
            builder.Services.AddValidatorsFromAssemblyContaining<FamilyValidation>();
            builder.Services.AddApiVersioning(setup =>
            {
                setup.ReportApiVersions = true;
            }).AddMvc();
            builder.Services.AddApiVersioning(setup =>
            {
                setup.DefaultApiVersion = new ApiVersion(1, 0);
                setup.AssumeDefaultVersionWhenUnspecified = true; 
                setup.ReportApiVersions = true;
            }).AddMvc();
            builder.Services.AddControllers();
            builder.Services.AddSingleton<IConfiguration>(builder.Configuration);


            builder.Services.AddMvc().AddFluentValidation();
            builder.Services.AddDbContext<FoodConnectDB>( option => 
            option.UseMySql( connectionString, ServerVersion.AutoDetect( connectionString ) ) );
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = builder.Configuration["config:JwtIssuer"],
                    ValidAudience = builder.Configuration["config:JwtAudience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["config:JwtKey"]))
                };
            });
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
