using ASMWeb.Models;
using ASMWeb.Utility;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASMWeb.DataAccess.DbInitializer
{
    public class DbInitializer : IDbInitializer
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DbInitializer(ApplicationDbContext db,
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _db = db;
            _roleManager = roleManager;
            _userManager = userManager;
        }
        public void Initialize()
        {
            try
            {
                if (_db.Database.GetPendingMigrations().Count() > 0)
                {
                    _db.Database.Migrate();
                }
            }
            catch (Exception)
            {

            }

            if (!_roleManager.RoleExistsAsync(SD.TeacherRole).GetAwaiter().GetResult())
            {
                _roleManager.CreateAsync(new IdentityRole(SD.AdminRole)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(SD.PrincipalRole)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(SD.TeacherRole)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(SD.SupportRole)).GetAwaiter().GetResult();

                _userManager.CreateAsync(new ApplicationUser
                {
                    UserName = "admin@tcdsb.org",
                    Email = "admin@tcdsb.org",
                    EmailConfirmed = true,
                    FirstName = "admin",
                    LastName = "tcdsb"
                }, "Tcdsb123*").GetAwaiter().GetResult();

                ApplicationUser user = _db.ApplicationUser.FirstOrDefault(u => u.Email == "admin@tcdsb.org");

                _userManager.AddToRoleAsync(user, SD.AdminRole).GetAwaiter().GetResult();



            }
            return;
        }
    }
}
