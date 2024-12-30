using BikeShop.Core.Manufacturers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BikeShop.Infrastructure.Data.Config;

public class ManufacturerConfiguration : IEntityTypeConfiguration<Manufacturer>
{
  public void Configure(EntityTypeBuilder<Manufacturer> builder)
  {
    builder.Property(p => p.Name)
        .HasMaxLength(DataSchemaConstants.DEFAULT_NAME_LENGTH)
        .IsRequired();
  }
}
