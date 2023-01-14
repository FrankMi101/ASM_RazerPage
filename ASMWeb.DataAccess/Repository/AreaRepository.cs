using ASMWeb.DataAccess.Repository;
using ASMWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASMWeb.DataAccess.Repository
{
    public class AreaRepository : Repository<Area>, IAreaRepository
    {
        private ApplicationDbContext _db;
        public AreaRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }      
    }
}
