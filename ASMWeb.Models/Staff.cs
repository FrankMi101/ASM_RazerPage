using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASMWeb.Models
{
    public class Staff
    {
        [Key]
        public int Id { get; set; }
   
        [Required]
        [Display(Name = "User ID")]
        [MaxLength(20)] 
        public string? UserID { get; set; }

        [Display(Name = "Employee ID")]
        [MaxLength(20)]
        [Required]
        public string? EmployeeID { get; set; }

        [Display(Name = "First Name")]
        [Required]
        [MaxLength(50)]
        public string? FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required]
        [MaxLength(50)]
        public string? LastName { get; set; }

        [Display(Name = "School Code")]
        [Required]
        [MaxLength(10)]
        public string? UnitID { get; set; }
        
        [MaxLength(20)]
        public string? Status { get; set; }

        [MaxLength(30)]
        public string RoleID { get; set; }

        [ForeignKey("RoleID")]
        [NotMapped]
        [ValidateNever]
        public AppRole AppRole { get; set; }

        [MaxLength(150)]
        public string? Position { get; set; }

        [MaxLength(100)]
        [Required]
        public string? Email { get; set; }


    }
}
