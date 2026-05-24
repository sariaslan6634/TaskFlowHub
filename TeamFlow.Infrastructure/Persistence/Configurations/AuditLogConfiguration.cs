using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamFlow.Domain.Entities;

namespace TeamFlow.Infrastructure.Persistence.Configurations
{
    public class AuditLogConfiguration : IEntityTypeConfiguration<AuditLog>
    {
        public void Configure(EntityTypeBuilder<AuditLog> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.EntityName).IsRequired().HasMaxLength(100);
            builder.Property(x => x.Action).IsRequired().HasMaxLength(100);
            builder.Property(x => x.OldValue).HasMaxLength(2000);
            builder.Property(x => x.NewValue).HasMaxLength(2000);

            builder.HasOne(x => x.User)
                .WithMany(x => x.AuditLogs)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.TaskItem)
                .WithMany(x => x.AuditLogs)
                .HasForeignKey(x => x.TaskItemId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
