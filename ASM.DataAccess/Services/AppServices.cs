
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASM.DataAccess.Repository
{
    public class AppServices : IAppServices
    {
        private readonly ApplicationDbContext _db;

        public AppServices(ApplicationDbContext db)
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
            GrantGroups = new GrantGroupRepository(db);
            Permissions = new PermissionRepository(db);
            Principals = new PrincipalRepository(db);
            Schools = new SchoolRepository(db);
            SchoolTypes = new SchoolTypeRepository(db);
            Scopes = new ScopeRepository(db);
            UserProfile = new UserProfileRepository(db);
            SapProfiles = new SapProfileRepository(db);
            WorkingSchools = new WorkingSchoolsRepository(db);
            PositionTypes = new PositionTypesRepository(db);
            NameValueItems = new NameValueList(db);
            SP_Call = new SP_Call();
            SP_CallWithDb = new SP_Call(db);
            SP_CallAny = new SP_Call();
        }

        public IAppRoleRepository AppRole { get; private set; }
        public IAppsRepository Apps { get; private set; }
        public IAppsModelsRepository AppModels { get; private set; }
        public IAreaRepository Areas { get; private set; }
        public IDeveloperRepository Developers { get; private set; }
        public IDistrictRepository Districts { get; private set; }
        public IEmployeeRepository Employees { get; private set; }
        public IGrantTypeRepository GrantTypes { get; private set; }
        public IGrantGroupRepository GrantGroups { get; private set; }
        public IPermissionRepository Permissions { get; private set; }
        public IPrincipalRepository Principals { get; private set; }
        public ISchoolRepository Schools { get; private set; }
        public ISchoolTypeRepository SchoolTypes { get; private set; }
        public IScopeRepository Scopes { get; private set; }
        public IUserProfileRepository UserProfile { get; private set; }
        public IWorkingSchoolsRepository WorkingSchools { get; private set; }
        public ISapProfileRepository SapProfiles { get; private set; }
        public IPositionTypesRepository PositionTypes { get; private set; }
        public INameValueList NameValueItems { get; private set; }
       
        public ISP_Call SP_Call { get; private set; }
        public ISP_Call SP_CallWithDb { get; private set; }
        public ISP_Call SP_CallAny { get; private set; }


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
