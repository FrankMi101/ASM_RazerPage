using System.ComponentModel.DataAnnotations;

namespace ASM.Models
{
    public class GrantGroup
    {  
         [Key]
       public int Id { get; set; }

        [Required]
        [MaxLength(20)] 
        public string? GroupCode { get; set; }

        [MaxLength(50)]
        public string? GroupName { get; set; }
    }
}
