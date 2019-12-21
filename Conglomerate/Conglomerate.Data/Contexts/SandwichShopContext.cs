using Conglomerate.Data.Entities.SandwichShop;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Conglomerate.Data.Contexts
{
    public class SandwichShopContext : DbContext
    {
        public DbSet<Ingredient> Ingredients { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite("Data Source=sandwichshop.db");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("sandwichshop.db")
                .Entity<Ingredient>(ConfigureIngredients);
        }

        private void ConfigureIngredients(EntityTypeBuilder<Ingredient> entity)
        {
            entity.ToTable("Ingredients");
            entity.HasKey(s => s.Id);
            entity.Property(s => s.Name)
                .IsRequired()
                .HasMaxLength(50);
            entity.Property(s => s.Price)
                .IsRequired();
            entity.HasIndex(s => s.Name)
                .IsUnique();
        }
    }
}
