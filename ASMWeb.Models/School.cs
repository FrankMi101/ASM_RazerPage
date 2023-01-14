using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASMWeb.Models
{
    public class School
    {
         [Key]
       public int Id { get; set; }
   
        [Display(Name = "School Code")]
        [MaxLength(10)] 
        public string? UnitCode { get; set; }

        [Display(Name = "School BSID")]
        [MaxLength(10)]
        [Required]
        public string? BSID { get; set; }

        [Display(Name = "School Name")]
        [Required]
        [MaxLength(350)]
        public string? UnitName { get; set; }

        [Display(Name = "School BriefName")]
        [MaxLength(100)]
        public string? BriefName { get; set; }

        [MaxLength(20)]
        public string? Status { get; set; }

        [MaxLength(30)]
        public string? PrincipalID { get; set; }

        [MaxLength(10)]
        public string? AreaCode { get; set; }
 
        [MaxLength(20)]
        public string? TypeCode { get; set; }

        [MaxLength(20)]
        public string? DistrictCode { get; set; }
    
        [MaxLength(300)]
        public string? Location { get; set; }


    }
}
