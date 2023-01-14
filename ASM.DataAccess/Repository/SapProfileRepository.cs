
using ASM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASM.DataAccess.Repository
{
    public class SapProfileRepository : Repository<SapProfile>, ISapProfileRepository
    {
        private ApplicationDbContext _db;
         public SapProfileRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
       
        public override object SPName(string action)
        {
            if (action == "") return "dbo.ASM_Staff_SAPProfile_Read";
            if (action == "Read") return "dbo.ASM_Staff_SAPProfile_Read";
            if (action == "Edit") return "dbo.ASM_WorkingSchools_Edit";
            return action;
        }
    }
}
