using Microsoft.EntityFrameworkCore;
using NotificationAndMessagingService.Models;
using System.Reflection.Metadata;

namespace NotificationAndMessagingService.Context
{
   
        public class FitnessContext : DbContext
        {
            public FitnessContext(DbContextOptions<FitnessContext> options) : base(options)
            {

            }

            public DbSet<WorkOutdayInfo> WorkOutdayInfos { get; set; }
            public DbSet<CheatMealInfo> CheatMealInfos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CheatMealInfo>()
                .ToTable(tb => tb.HasTrigger("SomeTrigger"));
        }
    }
    
}
