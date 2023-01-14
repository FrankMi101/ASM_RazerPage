using System.ComponentModel.DataAnnotations;

namespace ASMWeb.Models
{
    public class Principal
    {   
        [Key]
         public int Id { get; set; }

        [MaxLength(30)] 
        public string? PrincipalID { get; set; }

        [Required] 
        [MaxLength(100)]
        public string? PrincipalName { get; set; }
    }
}
