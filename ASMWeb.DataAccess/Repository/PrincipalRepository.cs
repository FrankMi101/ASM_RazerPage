using ASMWeb.DataAccess.Repository;
using ASMWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASMWeb.DataAccess.Repository
{
    public class PrincipalRepository : Repository<Principal>, IPrincipalRepository
    {
        private ApplicationDbContext _db;
        public PrincipalRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }      
    }
}
