using MeetingMinutes.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;

namespace MeetingMinutes.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Meeting> Meetings { get; set; }
        public DbSet<MeetingParticipant> MeetingParticipants { get; set; }
        public DbSet<MeetingItem> MeetingItems { get; set; }
        public DbSet<RiskLevel> RiskLevels { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Meeting>().ToTable("Meeting");
            modelBuilder.Entity<MeetingParticipant>().ToTable("MeetingParticipant");
            modelBuilder.Entity<MeetingItem>().ToTable("MeetingItem");
            modelBuilder.Entity<RiskLevel>().ToTable("RiskLevel");
        }
        
        public override Task<int> SaveChangesAsync(

            bool acceptAllChangesOnSuccess,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var AddedEntities = ChangeTracker.Entries()
                .Where(E => (E.State == EntityState.Added) && (E.Entity is Meeting))
                .ToList();

            AddedEntities.ForEach(E =>
            {
                E.Property("DateCreated").CurrentValue = DateTime.Now;
                E.Property("DateUpdated").CurrentValue = DateTime.Now;
                E.Property("Status").CurrentValue = Status.New;
            });

            var EditedEntities = ChangeTracker.Entries()
                .Where(E => (E.State == EntityState.Modified) && (E.Entity is Meeting))
                .ToList();

            EditedEntities.ForEach(E =>
            {
                E.Property("DateCreated").IsModified = false;
                E.Property("DateUpdated").CurrentValue = DateTime.Now;
            });

            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

    }
}
