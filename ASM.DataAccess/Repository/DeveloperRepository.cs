
using ASM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASM.DataAccess.Repository
{
    public class DeveloperRepository : Repository<Developer>, IDeveloperRepository
    {
        private ApplicationDbContext _db;
        public DeveloperRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
    
    }
}
