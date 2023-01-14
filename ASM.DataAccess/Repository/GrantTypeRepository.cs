
using ASM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASM.DataAccess.Repository
{
    public class GrantTypeRepository : Repository<GrantType>, IGrantTypeRepository
    {
        private ApplicationDbContext _db;
        public GrantTypeRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
    }
}
