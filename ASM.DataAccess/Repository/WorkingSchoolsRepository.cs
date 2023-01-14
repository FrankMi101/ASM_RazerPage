
using ASM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASM.DataAccess.Repository
{
    public class WorkingSchoolsRepository : Repository<WorkingSchools>, IWorkingSchoolsRepository
    {
        private ApplicationDbContext _db;
         public WorkingSchoolsRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
       
        public override object SPName(string action)
        {
            if (action == "") return "dbo.ASM_WorkingSchools_Read";
            if (action == "Read") return "dbo.ASM_WorkingSchools_Read";
            if (action == "Edit") return "dbo.ASM_WorkingSchools_Edit";
            if (action == "EditFromSchoolList") return "dbo.ASM_WorkingSchools_Edit";
            return action;
        }
    }
}
