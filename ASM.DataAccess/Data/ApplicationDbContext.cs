
using ASM.Models;
using Microsoft.EntityFrameworkCore;

namespace ASM.DataAccess
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

       // public DbSet<Employee> Employees { get; set; }
        //public DbSet<NameValue> NameValues { get; set; }
        public DbSet<Area> ASM_SchoolAreas { get; set; }
        public DbSet<District> ASM_Districts { get; set; }
        public DbSet<SchoolType> ASM_SchoolTypes { get; set; }
        public DbSet<Principal> ASM_Principals { get; set; }
        public DbSet<Developer> ASM_Developers { get; set; }
        public DbSet<GrantType> ASM_GrantTypes { get; set; }
        public DbSet<GrantGroup> ASM_GrantGroups { get; set; }
        public DbSet<Permission> ASM_Permissions { get; set; }
        public DbSet<Scope> ASM_Scopes { get; set; }


        public DbSet<School> ASM_Schools { get; set; }
        public DbSet<Employee> ASM_Employees { get; set; }
        public DbSet<Staff> ASM_Staffs { get; set; }
        public DbSet<AppRole> ASM_AppRole { get; set; }

        public DbSet<Apps> ASM_Apps { get; set; }
        public DbSet<AppsModels> ASM_AppModels { get; set; }

        // ond of use connectionstring
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("connectingstring");
        //    base.OnConfiguring(optionsBuilder);

        //}
    }
}
