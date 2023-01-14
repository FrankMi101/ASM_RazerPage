
using ASM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASM.DataAccess.Repository
{
    public class SchoolRepository : Repository<School>, ISchoolRepository
    {
        //private ApplicationDbContext _db;
         public SchoolRepository(ApplicationDbContext db) : base(db)
        {
          //  _db = db;
        }
        public void Update(School obj)
        {
          //  _db.ASM_Schools.Update(obj);

            //var objFromDb = _db.ASM_Schools.FirstOrDefault(u => u.Id == obj.Id);

            //objFromDb.DistrictCode = obj.DistrictCode;
            //objFromDb.PrincipalID = obj.PrincipalID;
            //objFromDb.AreaCode = obj.AreaCode;
            //objFromDb.TypeCode = obj.TypeCode;
            //_db.SaveChanges();
        }

        public override object SPName(string action)
        {
            if (action == "") return "dbo.ASM_SchoolList";
            if (action == "Read") return "dbo.ASM_SchoolList";
            if (action == "Edit") return "dbo.ASM_School_Edit";
            return action;
        }
    }
}
