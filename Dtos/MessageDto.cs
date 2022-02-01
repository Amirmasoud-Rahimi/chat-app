using ChatApp.Entities;

namespace ChatApp.Dtos
{
    public class MessageDto
    {
        public int Id { get; set; }
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }
        public string MessageContent { get; set; }
        public DateTime SendingDate { get; set; }
        public DateTime? ReceivingDate { get; set; }
        public string? Attachment { get; set; }

        public static MessageDto Mapper(Message message)
        {
            return new MessageDto
            {
                Id = message.MessageId,
                SenderId = message.SenderId,
                ReceiverId = message.ReceiverId,
                MessageContent = message.MessageContent,
                SendingDate = message.SendingDate,
                ReceivingDate = message.ReceivingDate,
                Attachment = message.Attachment
            };
        }
    }
}