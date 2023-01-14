using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASM.Models
{
    public class SapProfile
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(30)]
        public string? Operate { get; set; }

        [MaxLength(30)]
        public string? UserID { get; set; }

        [Display(Name = "Employee ID")]
        [MaxLength(15)]
        public string? EmployeeID { get; set; }

        [Required]
        [Display(Name = "Group Type")]
        [MaxLength(20)]
        public string? GroupType { get; set; } 

        [Required]
        [Display(Name = "Group")]
        [MaxLength(50)]
        public string? GroupValue { get; set; }

        [Display(Name = "School Name")]
        [MaxLength(350)]
        public string? GroupName { get; set; }


        [Display(Name = "Job Descption")]
        [MaxLength(350)]
        public string? Position { get; set; }

        [MaxLength(30)]
        public string? StaffUserID { get; set; }
        
        [Required]
        [MaxLength(30)]
        public string? AppID { get; set; }

        [Required]
        [Display(Name = "User Role")]
        [MaxLength(30)]
        public string? AppRole { get; set; }

        [Required]
        [Display(Name = "Start Date")]
        [BindProperty, DataType(DataType.Date)]
        public DateTime? StartDate { get; set; }

        [Required]
        [Display(Name = "End date")]
        [BindProperty, DataType(DataType.Date)]
        public DateTime? EndDate { get; set; }
  
        [MaxLength(300)]
        public string? Comments { get; set; }

        [MaxLength(300)]
        public string? Actions { get; set; }
      
        public int? RowNo { get; set; }
    }
}
