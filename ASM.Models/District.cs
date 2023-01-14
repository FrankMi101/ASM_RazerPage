using System.ComponentModel.DataAnnotations;

namespace ASM.Models
{
    public class District
    {   
         [Key]
        public int Id { get; set; }

        [MaxLength(10)] 
        public string? DistrictCode { get; set; }

        [Required] 
        [MaxLength(50)]
        public string? DistrictName { get; set; }
    }
}
