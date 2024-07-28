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
        public DbSet<Individual> Recipent => Set<Individual>();
        public DbSet<User> User => Set<User>();
        public DbSet<Role> Role => Set<Role>();
        public DbSet<Organisation> Organisation => Set<Organisation>();


       


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Role>().HasData(
                new Role {
                    Name = "Manager",
                    Description = "Manages the system of the application",
                },
                new Role {
                    Name = "FamilyHead", 
                    Description = "Registering on the application as a Head of a family. you will be in charge of all activity on the application on behalf of your Family. \n Pls note that once you register your family they can not register as an individual again"
                },
                new Role { 
                    Name = "OrganisationHead",
                            Description = "Registering on the application as a Head of an Organization. you will be in charge of all activity on the application on behalf of your Organization e.g NGO"
                },
                new Role
                {
                    Name = "Individual",
                    Description = "Registering on the application as an Individual. \nyou are incharge of every activity on the application"
                }
                ) ;

            //modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        }

    }
}
