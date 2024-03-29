using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Configuration;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(x=> x.Id);
        builder.Property(x=>x.Name).IsRequired().HasMaxLength(50);
        builder.HasOne(x=>x.User).WithMany(x=>x.Products).HasForeignKey(x=>x.OwnerId);
    }
}
