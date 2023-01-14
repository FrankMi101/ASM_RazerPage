using System.ComponentModel.DataAnnotations;

namespace ASM.Models
{
    public class GrantType
    {  
         [Key]
       public int Id { get; set; }

        [Required]
        [MaxLength(20)] 
        public string? TypeCode { get; set; }

        [MaxLength(250)]
        public string? Comments { get; set; }
    }
}
