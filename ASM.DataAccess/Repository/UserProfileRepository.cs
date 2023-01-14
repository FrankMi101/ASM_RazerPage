
using ASM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASM.DataAccess.Repository
{
    public class UserProfileRepository : Repository<UserProfile>, IUserProfileRepository
    {
        private ApplicationDbContext _db;
         public UserProfileRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
       
        public override object SPName(string action)
        {
            if (action == "") return "dbo.ASM_UserProfile";
            if (action == "Read") return "dbo.ASM_UserProfile";
            return action;
        }
    }
}
