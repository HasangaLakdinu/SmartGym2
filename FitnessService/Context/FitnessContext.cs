using FitnessService.Models;
using Microsoft.EntityFrameworkCore;

namespace FitnessService.Context
{
    public class FitnessContext : DbContext
    {
        public FitnessContext(DbContextOptions options) : base(options)
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
