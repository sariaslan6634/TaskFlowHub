using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamFlow.Domain.Common;

namespace TeamFlow.Domain.Entities
{
    public class Message :BaseEntity
    {
        public string Content { get; set; } = string.Empty;
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }
        public bool IsRead { get; set; } = false;

        // Navigation
        public User Sender { get; set; } = null!;
        public User Receiver { get; set; } = null!;
    }
}
