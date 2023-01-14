using ASMWeb.DataAccess.Repository;
using ASMWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASMWeb.DataAccess.Repository
{
    public class ScopeRepository : Repository<Scope>, IScopeRepository
    {
        private ApplicationDbContext _db;
        public ScopeRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }      
    }
}
