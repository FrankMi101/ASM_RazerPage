
using ASM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASM.DataAccess.Repository
{
    public class PositionTypesRepository : Repository<PositionType>, IPositionTypesRepository
    {
        private ApplicationDbContext _db;
        public PositionTypesRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public override object SPName(string action)
        {
            if (action == "") return "dbo.ASM_NameValueList";
            if (action == "Read") return "dbo.ASM_NameValueList"; 
            return action;
        }
    }
}
