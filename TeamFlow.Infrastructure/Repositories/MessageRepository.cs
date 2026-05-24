using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamFlow.Application.Interfaces.Repositories;
using TeamFlow.Domain.Entities;
using TeamFlow.Infrastructure.Persistence;

namespace TeamFlow.Infrastructure.Repositories
{
    public class MessageRepository : GenericRepository<Message>, IMessageRepository
    {
        public MessageRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Message>> GetConversationAsync(int senderId, int receiverId)
        {
            return await _dbSet
                .Where(x =>
                (x.SenderId == senderId && x.ReceiverId == senderId) ||
                (x.SenderId == receiverId && x.ReceiverId == senderId))
                .OrderBy(x => x.CreatedBy)
                .ToListAsync();
        }
    }
}
