using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASM.Models
{
    public class Apps
    { 
         [Key]
       public int Id { get; set; }

        [Required]
        [Display(Name = "App Code")]
        [MaxLength(10)]
        [MinLength(3)]
        public string? AppID { get; set; }

        [Required]
        [Display(Name ="App Name")]
        [MaxLength(150)]
        public string? AppName { get; set; }

        [MaxLength(30)]
        public string? DeveloperId { get; set; }

        [MaxLength(150)]
        public string? Owners { get; set; }

        [Display(Name = "Start Date")]
        //[DisplayFormat(DataFormatString = "{0:yyyy mm dd}")]
        [BindProperty, DataType(DataType.Date)]
        public DateTime? StartDate { get; set; }

        //[DisplayFormat(DataFormatString = "{0:yyyy mm dd}")]
        [BindProperty, DataType(DataType.Date)]
        public DateTime? EndDate { get; set; }


        [Display(Name = "App Web Site")]
        [MaxLength(250)]
        public string? AppUrl { get; set; }

        [Display(Name = "App Web Test Site")]
        [MaxLength(250)]
        public string? AppUrlTest { get; set; }

        [Display(Name = "App Web Train Site")]
        [MaxLength(250)]
        public string? AppUrlTrain { get; set; }

        [MaxLength(500)]
        public string? Comments { get; set; }

        [Display(Name = "Last Update Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy mm dd}")]
        public DateTime? LastUpdateDate { get; set; }

        [MaxLength(50)]
        public string? LastUpdateUser { get; set; }


    }
}
