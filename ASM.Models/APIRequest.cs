using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASM.Models
{
    public class APIRequest
    {
        public string APIAction { get; set; } = "GET";
        public string Url { get; set; }
        public object Data { get; set; }
        public string  Token { get; set; }
    }
}
