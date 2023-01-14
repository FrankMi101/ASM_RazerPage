using ASMWeb.DataAccess.Repository;
using ASMWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASMWeb.DataAccess.Repository
{
    public class AppRoleRepository : Repository<AppRole>, IAppRoleRepository
    {
        private ApplicationDbContext _db;
        public AppRoleRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public override object SPName(string action)
        {
            if (action == "") return "dbo.ASM_AppRole_Read";
            if (action == "Read") return "dbo.ASM_AppRole_Read";
            if (action == "Edit") return "dbo.ASM_AppRole_Edit";
            return action;
        }
    }
}
