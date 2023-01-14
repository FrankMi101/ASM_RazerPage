using System.ComponentModel.DataAnnotations;

namespace ASM.Models
{
    public class Permission
    {  
        [Key]
        public int Id { get; set; }

        [Required] 
        public int Priority { get; set; }
        [Required]
        [MaxLength(20)] 
        public string? PermissionType { get; set; }

        [MaxLength(250)]
        public string? Comments { get; set; }
    }
}
