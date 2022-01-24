using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChatApp.Entities
{
    [Table("Users")]
    public partial class User // partial?
    {
        public int UserId { get; set; }
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string FullName { get; set; }
        public DateTime? DateOfJoining { get; set; }
        public string? PhtotFileName { get; set; }
    }
}