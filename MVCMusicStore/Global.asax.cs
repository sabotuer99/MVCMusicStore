using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using MVCMusicStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace MVCMusicStore
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            System.Data.Entity.Database.SetInitializer(new MVCMusicStore.Models.SampleData());

            //If this shit works it'll add an Administrator user and add the administrator role...
            var db = new ApplicationDbContext();
            UserManager<ApplicationUser> um = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            RoleManager<IdentityRole> rm = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            var user = new ApplicationUser() { UserName = "Administrator" };
            var result = um.Create(user, "password123!");
            var user2 = new ApplicationUser() { UserName = "Troy" };
            result = um.Create(user2, "password123!");
            if (!rm.RoleExists("Administrator"))
            {
                var role = new ApplicationRole() { Name = "Administrator" };
                rm.Create(role);
            }
            var adminId = db.Users.Where(x => x.UserName == "Administrator").Select(x => x.Id).SingleOrDefault();
            if (!um.IsInRole(adminId, "Administrator"))
            {
                um.AddToRole(adminId, "Administrator");
            }

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
