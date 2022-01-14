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
            var AddedMeetingEntities = ChangeTracker.Entries()
                .Where(E => (E.Entity is Meeting) && (E.State == EntityState.Added) )
                .ToList();

            AddedMeetingEntities.ForEach(E =>
            {
                E.Property("DateCreated").CurrentValue = DateTime.Now;
                E.Property("DateUpdated").CurrentValue = DateTime.Now;
                if (DateTime.Parse(E.Property("MeetingDate").CurrentValue.ToString()) > DateTime.Now)
                {
                    E.Property("Status").CurrentValue = Status.New;
                }
                else {
                    E.Property("Status").CurrentValue = Status.Finished;
                }
            });

            var AddedMeetingItemsEntities = ChangeTracker.Entries()
                .Where(E => (E.Entity is MeetingItem) && (E.State == EntityState.Added))
                .ToList();

            AddedMeetingItemsEntities.ForEach(E =>
            {
                var Meeting = ChangeTracker.Entries().Where(M => M.Property("MeetingID") == E.Property("Meetingid").CurrentValue).ToList();
                Meeting.ForEach(E => E.Property("Status").CurrentValue = Status.Started);
            });

            var EditedMeetingEntities = ChangeTracker.Entries()
                .Where(E => (E.Entity is Meeting) && (E.State == EntityState.Modified) )
                .ToList();

            EditedMeetingEntities.ForEach(E =>
            {
                E.Property("DateCreated").IsModified = false;
                E.Property("DateUpdated").CurrentValue = DateTime.Now;
                if (DateTime.Parse(E.Property("MeetingDate").CurrentValue.ToString()) > DateTime.Now)
                {
                    E.Property("Status").CurrentValue = Status.New;
                }
                else
                {
                    E.Property("Status").CurrentValue = Status.Finished;
                }
            });

            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

    }
}
