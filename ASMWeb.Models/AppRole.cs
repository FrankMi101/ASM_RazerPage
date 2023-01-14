using System.ComponentModel.DataAnnotations;

namespace ASMWeb.Models
{
    public class AppRole
    {
        public int Id { get; set; }
        [Key]
        [MaxLength(30)]
        public string? RoleID { get; set; }


        [Required]
        [MaxLength(100)]
        public string? RoleName { get; set; }

        [Required]
        public int Priority { get; set; }

    }
}
