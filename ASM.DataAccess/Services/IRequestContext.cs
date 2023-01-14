
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASM.DataAccess.Services
{
    public interface IRequestContext
    {
        string CorrelationId { get; }
    }
   
}
