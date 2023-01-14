using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASM.DataAccess.Repository
{
    public interface IAppActionsCatalog : IDisposable
    {

        IAppsRepository Apps { get; }
        IAppsModelsRepository AppModels { get; }
        IAreaRepository Areas { get; }
        IDeveloperRepository Developers { get; }
        IDistrictRepository Districts { get; }
        IEmployeeRepository Employees { get; }
        IGrantTypeRepository GrantTypes { get; }
        IGrantGroupRepository GrantGroups { get; }
        IPermissionRepository Permissions { get; }
        IPrincipalRepository Principals { get; }
        ISchoolRepository Schools { get; }
        ISchoolTypeRepository SchoolTypes { get; }
        IScopeRepository Scopes { get; }
        IAppRoleRepository AppRole { get; }
        IUserProfileRepository UserProfile { get; }
        ISapProfileRepository SapProfiles { get; }
        IWorkingSchoolsRepository WorkingSchools { get; }
        ISP_Call SP_Call { get; }

        void Save();
    }
}
