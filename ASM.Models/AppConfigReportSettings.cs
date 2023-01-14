using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASM.Models
{
    public class AppConfigReportSettings
    {
        public string ReportServer { get; set; }
        public string ReportPath { get; set; }
        public string ReportFormat { get; set; }
        public string ReportUser { get; set; }
        public string ReportPW { get; set; }

        public string  Domain { get; set; }
    }
}
