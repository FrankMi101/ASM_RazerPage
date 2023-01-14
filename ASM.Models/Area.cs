using System.ComponentModel.DataAnnotations;

namespace ASM.Models
{
    public class Area
    {   
         [Key]
       public int Id { get; set; }

        [MaxLength(10)] 
        public string? AreaCode { get; set; }


        [Required]
        [MaxLength(50)]
        public string? AreaName { get; set; }

        [Required] 
        [MaxLength(30)]
        public string? SupervisorID { get; set; }

        [Required]
        [MaxLength(30)]
        public string? OfficerID { get; set; }
    }
}
