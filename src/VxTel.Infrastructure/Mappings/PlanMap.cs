using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VxTel.Domain.Entities;

namespace VxTel.Infrastructure.Mappings
{
    public class PlanMap : IEntityTypeConfiguration<Plan>
    {
        public void Configure(EntityTypeBuilder<Plan> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name)
                .IsRequired()
                .HasColumnType("varchar")
                .HasMaxLength(50);

            builder.Property(p => p.Minutes)
                .IsRequired();

            builder.Property(p => p.CreatedAt)
                .HasColumnType("datetime");

            builder.Property(p => p.UpdatedAt)
                .HasColumnType("datetime");

            builder.ToTable("Plans");
        }
    }
}
