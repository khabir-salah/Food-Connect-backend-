using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.persistence.Context
{
    public class FoodConnectDB : DbContext
    {
        public FoodConnectDB(DbContextOptions<FoodConnectDB> option) : base(option) { }

        public DbSet<Donation> Donation => Set<Donation>();
        public DbSet<Family> Family => Set<Family>();
        public DbSet<FoodCollection> FoodCollection => Set<FoodCollection>();
        public DbSet<Manager> Manager => Set<Manager>();
        public DbSet<Recipent> Recipent => Set<Recipent>();
        public DbSet<User> User => Set<User>();
        public DbSet<Role> Role => Set<Role>();
        public DbSet<Organisation> Organisation => Set<Organisation>();


       


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Role>().HasData(
                new Role {
                    Id = Guid.NewGuid(),
                    Name = "Manager",
                    Description = "Manages the system of the application",
                },
                new Role {
                    Id = Guid.NewGuid(),
                    Name = "Family", 
                    Description = "Registering on the application as a whole family" 
                },
                new Role { 
                    Id = Guid.NewGuid(),
                    Name = "Organisation",
                            Description = "Registering on the application as a whole Organization e.g NGO"
                },
                new Role
                {
                    Id = Guid.NewGuid(),
                    Name = "Recipent",
                    Description = "Registering on the application as an Individual"
                }
                ) ;

            //modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        }

    }
}
