using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASM.Models
{
    public class AppsModels
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(10)]
        [MinLength(3)]
        public string? AppID { get; set; }     

        [Display(Name = "Model ID")]
        [MaxLength(20)]
        [Required]
        public string? ModelID { get; set; }

        [Display(Name = "Model Name")]
        [Required]
        [MaxLength(100)]
        public string? ModelName { get; set; }

        [Required]
        [MaxLength(30)]
        public string? DeveloperId { get; set; }

        [Required]
        [MaxLength(30)]
        public string? TypeCode { get; set; }


        [MaxLength(150)]
        public string? Owners { get; set; }

        [BindProperty, DataType(DataType.Date)]
        [Display(Name = "Start Date")]
        //[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }

        [BindProperty, DataType(DataType.Date)]
        [Display(Name = "End Date")]
       //[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime EndDate { get; set; }

        [MaxLength(500)]
        public string? Comments { get; set; }


        [Display(Name = "Last Update Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy mm dd}")]
        public DateTime? LastUpdateDate { get; set; }

        [MaxLength(50)]
        public string? LastUpdateUser { get; set; }


    }
}
