using ASMWeb.DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASMWeb.DataAccess.Repository
{
    public class AppActionsCatalog : IAppActionsCatalog
    {
        private readonly ApplicationDbContext _db;

        public AppActionsCatalog(ApplicationDbContext db)
        {
            _db = db;
            AppRole = new AppRoleRepository(db);
            Apps = new AppsRepository(db);
            AppModels = new AppsModelsRepository(db);
            Areas = new AreaRepository(db);
            Developers = new DeveloperRepository(db);
            Districts = new DistrictRepository(db);
            Employees = new EmployeeRepository(db);
            GrantTypes = new GrantTypeRepository(db);
            Permissions = new PermissionRepository(db);
            Principals = new PrincipalRepository(db);
            Schools = new SchoolRepository(db);
            SchoolTypes = new SchoolTypeRepository(db);
            Scopes = new ScopeRepository(db);
            ApplicationUser = new ApplicationUserRepository(db);
            SP_Call = new SP_Call(db);
        }

        public IAppsRepository Apps { get; private set; }
        public IAppsModelsRepository AppModels { get; private set; }
        public IAreaRepository Areas { get; private set; }
        public IDeveloperRepository Developers { get; private set; }
        public IDistrictRepository Districts { get; private set; }
        public IEmployeeRepository Employees { get; private set; }
        public IGrantTypeRepository GrantTypes { get; private set; }
        public IPermissionRepository Permissions { get; private set; }
        public IPrincipalRepository Principals { get; private set; }
        public ISchoolRepository Schools { get; private set; }
        public ISchoolTypeRepository SchoolTypes { get; private set; }
        public IScopeRepository Scopes { get; private set; }
        public IApplicationUserRepository ApplicationUser { get; private set; }
       public  IAppRoleRepository AppRole { get; private set; }
        public ISP_Call SP_Call { get; private set; }


        public void Dispose()
        {
            _db.Dispose();
        }
        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
