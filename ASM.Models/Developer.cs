using System.ComponentModel.DataAnnotations;

namespace ASM.Models
{
    public class Developer
    {   
       [Key]
       public int Id { get; set; }

        [MaxLength(30)] 
        public string?  UserID { get; set; }

        [Required] 
        [MaxLength(100)]
        public string? UserName { get; set; }
    }
}
