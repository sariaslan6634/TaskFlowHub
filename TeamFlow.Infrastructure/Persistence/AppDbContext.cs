using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamFlow.Domain.Entities;

namespace TeamFlow.Infrastructure.Persistence
{
    public class AppDbContext : IdentityDbContext<User,IdentityRole<int>,int>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Team> Teams { get; set; }
        public DbSet<TeamMember> TeamMembers { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Sprint> Sprints { get; set; }
        public DbSet<TaskItem> TaskItems { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<AuditLog> AuditLogs { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Tüm configuration dosyalarını otomatik uygula
            builder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

            // Soft delete global filtresi — IsDeleted = true olanlar sorgularda çıkmaz
            builder.Entity<Team>().HasQueryFilter(x => !x.IsDeleted);
            builder.Entity<Project>().HasQueryFilter(x => !x.IsDeleted);
            builder.Entity<Sprint>().HasQueryFilter(x => !x.IsDeleted);
            builder.Entity<TaskItem>().HasQueryFilter(x => !x.IsDeleted);
            builder.Entity<Comment>().HasQueryFilter(x => !x.IsDeleted);
            builder.Entity<Message>().HasQueryFilter(x => !x.IsDeleted);
            builder.Entity<Notification>().HasQueryFilter(x => !x.IsDeleted);
            builder.Entity<AuditLog>().HasQueryFilter(x => !x.IsDeleted);
            builder.Entity<User>().HasQueryFilter(x => !x.IsDeleted);
        }
    }
}
