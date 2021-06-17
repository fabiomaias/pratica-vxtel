using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VxTel.Domain.Entities;

namespace VxTel.Infrastructure.Mappings
{
    public class PriceMap : IEntityTypeConfiguration<Price>
    {
        public void Configure(EntityTypeBuilder<Price> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Origin)
                .IsRequired()
                .HasMaxLength(3)
                .HasColumnType("varchar");

            builder.Property(p => p.Destination)
                .IsRequired()
                .HasMaxLength(3)
                .HasColumnType("varchar");

            builder.Property(p => p.Charge)
                .IsRequired();

            builder.Property(p => p.CreatedAt)
                .HasColumnType("datetime");

            builder.Property(p => p.UpdatedAt)
                .HasColumnType("datetime");

            builder.ToTable("Prices");
        }
    }
}
