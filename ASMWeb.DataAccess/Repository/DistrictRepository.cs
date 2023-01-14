using ASMWeb.DataAccess.Repository;
using ASMWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASMWeb.DataAccess.Repository
{
    public class DistrictRepository : Repository<District>, IDistrictRepository
    {
        private ApplicationDbContext _db;
        public DistrictRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }      
    }
}
