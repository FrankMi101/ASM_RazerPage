using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASM.Models
{
    public class AppConfigConnectSettings
    {
        public string CurrentDb { get; set; }
        public string Localhost { get; set; }
        public string Live { get; set; }
        public string Test { get; set; }
        public string Default { get; set; }
        public string Develop { get; set; }
        public string APIUrl { get; set; }
    }
}
