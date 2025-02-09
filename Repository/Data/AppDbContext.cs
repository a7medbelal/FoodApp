
using AutoMapper.Configuration.Annotations;
using FoodApp.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Data
{
    public class Context : IdentityDbContext<ApplicationUser>

    {
        public Context(DbContextOptions<Context> options) : base(options) { }


        public DbSet<ApplicationUser> User { get; set; }
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<RecipeTag> RecipeTags { get; set; }

        public DbSet<Item> items { get; set; }  
        public DbSet<RoleFeature> RoleFeature { get; set; }
        public DbSet<Favioret> faviorets { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
       

        var password = "Admin@123";
            var hasher = new PasswordHasher<string>();
            var hashedpassword = hasher.HashPassword(null, password); 

            modelBuilder.Entity<ApplicationUser>().HasData(
                new ApplicationUser
                {
                    Id="Ahmedbela*-l117200@4@872004",
                    Email="legendahmed.122@gmail.com",
                    EmailConfirmed=true,
                    UserName="ahmed@7",
                    Fname ="Ahmed",
                    Lname="belal",
                    AccessFailedCount=0,
                    NormalizedUserName="AHMED@7",
                    NormalizedEmail ="LEGENDAHMED.122@GMAIL.COM",
                    LockoutEnabled=true,
                    TwoFactorEnabled=false,
                    roles= FoodApp.Core.Enums.Role.Admin , 
                    PasswordHash=hashedpassword,
                   });



            base.OnModelCreating(modelBuilder);



        }




    }
}
