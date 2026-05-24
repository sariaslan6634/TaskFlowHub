using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamFlow.Application.DTOs.Message
{
    public class MessageResponseDto
    {
        public int Id { get; set; }
        public string Content { get; set; } = string.Empty;
        public int SenderId { get; set; }
        public string SenderFullName { get; set; } = string.Empty;
        public bool IsRead { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
