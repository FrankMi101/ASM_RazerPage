using ASMWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASMWeb.DataAccess.Repository
{
    public  interface IEmployeeRepository : IRepository<Employee>
    {
      // void Update(Employee obj);
        //void Save();
    }
}
