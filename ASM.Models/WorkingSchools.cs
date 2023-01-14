using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASM.Models
{
    public class WorkingSchools
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(30)]
        public string? Operate { get; set; }

        [MaxLength(30)]
        public string? UserID { get; set; }

         [MaxLength(20)]
        public string? GroupType { get; set; }

        [MaxLength(50)]
        public string? GroupValue { get; set; }

       [MaxLength(30)]
        public string? StaffUserID { get; set; }
        [MaxLength(30)]
        public string? AppID { get; set; }

        [MaxLength(30)]
        public string? AppRole { get; set; }


        [MaxLength(15)]
        public string? StartDate { get; set; }

        [MaxLength(15)]
        public string? EndDate { get; set; }

        [MaxLength(10)]
        public string? Selected { get; set; }

        [Display(Name = "School Code")]
        [MaxLength(10)]
        public string? UnitCode { get; set; }
 
        [Display(Name = "School Name")]
        [Required]
        [MaxLength(350)]
        public string? UnitName { get; set; }

        [MaxLength(10)]
        public string? AreaCode { get; set; }

        [MaxLength(20)]
        public string? TypeCode { get; set; }

        [MaxLength(20)]
        public string? DistrictCode { get; set; }

        [MaxLength(300)]
        public string? Actions { get; set; }

    }
}
