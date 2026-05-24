using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamFlow.Application.DTOs.Message
{
    public class SendMessageDto
    {
        public string Content { get; set; } = string.Empty;
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }
    }
}
