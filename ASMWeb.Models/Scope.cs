using System.ComponentModel.DataAnnotations;

namespace ASMWeb.Models
{
    public class Scope
    { 
         [Key]     
       public int Id { get; set; }

        [Required]
        public int Priority { get; set; }

        [Display(Name = "Access Scope")]
        [MaxLength(20)] 
        public string? AccessScope { get; set; }

        [MaxLength(250)]
        public string? Comments { get; set; }

    }
}
