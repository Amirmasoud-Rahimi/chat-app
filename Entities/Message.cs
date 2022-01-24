using System.ComponentModel.DataAnnotations.Schema;

namespace ChatApp.Entities
{
    [Table("Message")]
    public partial class Message
    {
        public int MessageId { get; set; }
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }
        public string MessageContent { get; set; }
        public DateTime SendingDate { get; set; }
        public DateTime? ReceivingDate { get; set; }
        public string? Attachment { get; set; }
    }
}