using Application.Features.Interfaces.IRepositries;
using Domain.Constant;
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
        public DbSet<FamilyHead> FamilyHead => Set<FamilyHead>();
        public DbSet<FoodCollection> FoodCollection => Set<FoodCollection>();
        public DbSet<Manager> Manager => Set<Manager>();
        public DbSet<Individual> Individual => Set<Individual>();
        public DbSet<User> User => Set<User>();
        public DbSet<Message> Message => Set<Message>();
        public DbSet<Role> Role => Set<Role>();
        public DbSet<Organisation> Organisation => Set<Organisation>();



       

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            var password = "1234567";
            var salt = BCrypt.Net.BCrypt.GenerateSalt(10);
            var hashPassword = BCrypt.Net.BCrypt.HashPassword(password, salt);
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Name = "Salah",
                    Address = "EVerywhere",
                    Email = "admin@gmail.com",
                    IsActivated = true,
                    IsEmailConfirmed = true,
                    Password = hashPassword,
                    PhoneNumber = "1234567890",
                    RoleId = "00100",
                    ProfileImage = "fineboy",
                });

            modelBuilder.Entity<Role>().HasData(
                new Role {
                    RoleId = "317782478",
                    Name = RoleConst.Manager,
                    Description = "Manages the system of the application",
                },
                new Role {
                    RoleId = "5746724647",
                    Name = RoleConst.FamilyHead, 
                    Description = "Registering on the application as a Head of a family. you will be in charge of all activity on the application on behalf of your Family. \n Pls note that once you register your family they can not register as an individual again"
                },
                new Role {
                    RoleId = "537676327",
                    Name = RoleConst.OrganizationHead,
                            Description = "Registering on the application as a Head of an Organization. you will be in charge of all activity on the application on behalf of your Organization e.g NGO"
                },
                new Role
                {
                    RoleId = RoleConst.Individual,
                    Name = "Individual",
                    Description = "Registering on the application as an Individual. \nyou are incharge of every activity on the application"
                },
                 new Role
                 {
                     RoleId = "2678834",
                     Name = "Family",
                     Description = "Registering under the Family Head. \nThe Family Head is still incharge of every activity on the application"
                 },
                  new Role
                  {
                      RoleId = "00100",
                      Name = RoleConst.Admin,
                      Description = "Baba Alaye"
                  }
                ) ;

            //modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        }

    }
}
