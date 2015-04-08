namespace MyPersonalPage.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using MyPersonalPage.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<MyPersonalPage.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(MyPersonalPage.Models.ApplicationDbContext context)
        {
            var roleManager = new RoleManager<IdentityRole>(
                new RoleStore<IdentityRole>(context));
            
            if(!context.Roles.Any(r=>r.Name == "Admin"))
            {
                roleManager.Create(new IdentityRole { Name = "Admin"});
            }
            
            var userManager = new UserManager<ApplicationUser>(
                new UserStore<ApplicationUser>(context));

            if(!context.Users.Any(r=>r.Email == "markegaines@gmail.com"))
            {
                userManager.Create(new ApplicationUser
                { 
                    UserName = "markegaines@gmail.com",
                    Email="markegaines@gmail.com",
                    FirstName = "Mark",
                    LastName = "Gaines",
                    DisplayName = "Mark Gaines"
                }, "Plugh4!");               
            }

            var userId = userManager.FindByEmail("markegaines@gmail.com").Id;
            userManager.AddToRole(userId, "Admin");


            
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }
}
