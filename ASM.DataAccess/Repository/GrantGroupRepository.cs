
using ASM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASM.DataAccess.Repository
{
    public class GrantGroupRepository : Repository<GrantGroup>, IGrantGroupRepository
    {
        private ApplicationDbContext _db;
        public GrantGroupRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
    }
}
