using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChatApp.Entities
{
    [Table("Messages")]
    public partial class Message
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MessageId { get; set; }

        [Display(Name = "Sender Id")]
        [Required(ErrorMessage = "Sender Id is required")]
        public int SenderId { get; set; }

        [Display(Name = "Receiver Id")]
        [Required(ErrorMessage = "Receiver Id is required")]
        public int ReceiverId { get; set; }

        [StringLength(250,ErrorMessage ="Invalid Length")]
        [Display(Name = "Message Content")]
        [Required(ErrorMessage = "Message Content is required")]
        public string MessageContent { get; set; }

        [Display(Name = "Sending Date")]
        [Required(ErrorMessage = "Sending Date is required")]
        public DateTime SendingDate { get; set; }

        [Display(Name = "Receiving Date")]
        public DateTime? ReceivingDate { get; set; }

        [Display(Name ="Attachment")]
        public string? Attachment { get; set; }
    }
}