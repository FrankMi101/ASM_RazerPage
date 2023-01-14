
using ASM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASM.DataAccess.Repository
{
    public class NameValueList : Repository<NameValue>, INameValueList
    {
         public NameValueList(ApplicationDbContext db) : base(db)
        { 
        }
        public override object SPName(string action)
        {
           return "dbo.ASM_NameValueList"; 
        }
    }
}
