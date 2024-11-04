using Microsoft.EntityFrameworkCore;

namespace CO2EmissionsAPI.Models
{
    public partial class WorldBankEmissionsContext : DbContext
    {
        public WorldBankEmissionsContext()
        {
        }

        public WorldBankEmissionsContext(DbContextOptions<WorldBankEmissionsContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Country> Countries { get; set; } = null!;
        public virtual DbSet<EmissionsDatum> EmissionsData { get; set; } = null!;
        public virtual DbSet<EmissionsIndicator> EmissionsIndicators { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite("Data Source=Data/worldBankEmissions.db");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Country>(entity =>
            {
                entity.HasIndex(e => e.IsoCode, "IX_Countries_IsoCode")
                    .IsUnique();
            });

            modelBuilder.Entity<EmissionsDatum>(entity =>
            {
                entity.Property(e => e.Date).HasColumnType("DATETIME");

                entity.Property(e => e.EmissionsValue).HasColumnType("NUMERIC(8, 4)");

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.EmissionsData)
                    .HasForeignKey(d => d.CountryId);
            });

            modelBuilder.Entity<EmissionsIndicator>(entity =>
            {
                entity.HasIndex(e => e.Code, "IX_EmissionsIndicators_Code")
                    .IsUnique();
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
