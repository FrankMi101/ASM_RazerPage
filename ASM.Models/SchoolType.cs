using System.ComponentModel.DataAnnotations;

namespace ASM.Models
{
    public class SchoolType
    {   
        [Key]
         public int Id { get; set; }

        [MaxLength(20)] 
        public string? TypeCode { get; set; }

        [Required] 
        [MaxLength(20)]
        public string? TypeName { get; set; }
    }
}
