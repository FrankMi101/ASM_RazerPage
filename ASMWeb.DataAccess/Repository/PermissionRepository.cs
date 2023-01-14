using ASMWeb.DataAccess.Repository;
using ASMWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASMWeb.DataAccess.Repository
{
    public class PermissionRepository : Repository<Permission>, IPermissionRepository
    {
        private ApplicationDbContext _db;
        public PermissionRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }      
    }
}
