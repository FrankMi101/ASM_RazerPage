using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASM.Models
{
    public  class AppsUser : IdentityUser
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "User ID")]
        [MaxLength(20)]
        public string? UserID { get; set; }
 
        [Display(Name = "First Name")]
        [MaxLength(50)]
        public string? FirstName { get; set; }

        [Display(Name = "Last Name")]
        [MaxLength(50)]
        public string? LastName { get; set; }

        [Display(Name = "School Code")]
        [MaxLength(10)]
        public string? UnitID { get; set; }


        [MaxLength(30)]
        public string RoleID { get; set; }
 
    }
}
