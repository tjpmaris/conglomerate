using Conglomerate.Data.Entities.SandwichShop;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Conglomerate.Data.Contexts
{
    public class SandwichShopContext : DbContext
    {
        public DbSet<Ingredient> Ingredients { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseMySql("Server=localhost;Port=3306;Database=SandwichShop;Uid=root;Pwd=password;");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Ingredient>(ConfigureIngredients);
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
