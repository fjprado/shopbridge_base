using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Shopbridge_base.Domain.Models;

namespace Shopbridge_base.Data.Configuration
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(p => p.Product_Id);

            builder.Property(p => p.Product_Name)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(p => p.Product_Description)
                .IsRequired();

            builder.Property(p => p.Product_Price)
                .IsRequired()
                .HasPrecision(2);

            builder.Property(p => p.Product_StockBalance)
                .IsRequired()
                .HasPrecision(4);
        }
    }
}
