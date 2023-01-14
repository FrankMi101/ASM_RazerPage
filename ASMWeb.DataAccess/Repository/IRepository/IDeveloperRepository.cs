using ASMWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASMWeb.DataAccess.Repository
{
    public  interface IDeveloperRepository : IRepository<Developer>
    {
      // void Update(Developer obj);
        //void Save();
    }
}
