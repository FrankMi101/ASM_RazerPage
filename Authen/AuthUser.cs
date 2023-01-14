using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authen
{
    public class AuthUser
    {
        [Required]
        public string UserID { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        public string AppID { get; set; }
        public string AuthMethod { get; set; }
        public string UnitID { get; set; }
        public string AppRole { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
