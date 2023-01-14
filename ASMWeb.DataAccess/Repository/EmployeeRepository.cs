using ASMWeb.DataAccess.Repository;
using ASMWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASMWeb.DataAccess.Repository
{
    public class EmployeeRepository : Repository<Employee>, IEmployeeRepository
    {
        private ApplicationDbContext _db;
        public EmployeeRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public override object SPName(string action)
        {
            if (action == "") return "dbo.ASM_StaffList_Search";
            if (action == "Role") return "dbo.ASM_Staff_Role";
            if (action == "Read") return "dbo.ASM_Staff_Read";
            if (action == "Edit") return "dbo.ASM_Staff_Edit";
            return action;
        }
    }
}
