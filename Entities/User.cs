using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChatApp.Entities
{
    [Table("Users")]
    public partial class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; private set; }

        [Display(Name = "User Name")]
        [Required(ErrorMessage ="User Name is Required")]
        public string UserName { get; set; }

        [Required]
        [Display(Name = "Password")]
        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$",
            ErrorMessage = "Minimum eight characters, at least one letter and one number required")]
        public string Password { get; set; }

        [Display(Name = "Full Name")]
        [Required(ErrorMessage = "User Name is Required")]
        public string FullName { get; set; }

        [Display(Name ="Date Of Joining")]
        [Required(ErrorMessage = "DateOfJoining is Required")]
        public DateTime DateOfJoining { get; set; }

        [Display(Name ="Photo File Name")]
        public string? PhtotFileName { get; set; }
    }
}