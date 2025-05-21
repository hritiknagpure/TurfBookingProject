using System.ComponentModel.DataAnnotations;

namespace LoginFormASPcore.Models
{
    public class UserTbl
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = null!;
        [Required]
        public string Gender { get; set; } = null!;
        [Required]
        public int? Age { get; set; }
        public string Email { get; set; } = null!;
        [DataType(DataType.Password)]
        [Required]
        public string Password { get; set; } = null!;
    }
}


