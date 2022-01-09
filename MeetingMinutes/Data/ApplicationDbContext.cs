using MeetingMinutes.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

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
    }
}
