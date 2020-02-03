using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RunningLogApp.Website.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RunningLogApp.Website.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Athlete> Athlete { get; set; }
        public DbSet<StravaActivity> Activities { get; set; }
        public DbSet<MonthlySummary> MonthlySummaries { get; set; }
        public DbSet<TotalsData> TotalsData { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Athlete>()
                .HasOne(a => a.RecentRunTotals)
                .WithMany()
                .HasForeignKey(u => u.RecentRunTotalsId);

            builder.Entity<Athlete>()
                .HasOne(a => a.YearToDateRunTotals)
                .WithMany()
                .HasForeignKey(u => u.YearToDateRunTotalsId);

            builder.Entity<Athlete>()
                .HasOne(a => a.AllRunTotals)
                .WithMany()
                .HasForeignKey(u => u.AllRunTotalsId);
        }
    }
}
