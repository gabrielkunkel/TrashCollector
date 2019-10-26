using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Owin;
using TrashCollector.Models;
using Microsoft.AspNet.Identity.EntityFramework;

[assembly: OwinStartupAttribute(typeof(TrashCollector.Startup))]
namespace TrashCollector
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            CreateRoles();
        }

        private void CreateRoles()
        {
            ApplicationDbContext dbContext = new ApplicationDbContext();
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(dbContext));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(dbContext));


            if (roleManager.RoleExists("Admin") == false)
            {
                var role = new IdentityRole();
                role.Name = "Admin";
                roleManager.Create(role);

                var user = new ApplicationUser();
                user.UserName = "gabrielkunkel";
                user.Email = "henrysaltandson@gmail.com";

                var chkUser = userManager.Create(user, "password");

                if (chkUser.Succeeded)
                {
                    userManager.AddToRole(user.Id, "Admin");
                }

            }

            if(roleManager.RoleExists("Customer") == false)
            {
                var role = new IdentityRole();
                role.Name = "Customer";
                roleManager.Create(role);
            }

            if (roleManager.RoleExists("Employee") == false)
            {
                var role = new IdentityRole();
                role.Name = "Employee";
                roleManager.Create(role);
            }

        }

    }


}
