using BikeShop.Core.BikeModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BikeShop.Infrastructure.Data.Config;

public class BikeModelConfiguration : IEntityTypeConfiguration<BikeModel>
{
  public void Configure(EntityTypeBuilder<BikeModel> builder)
  {
    builder.Property(p => p.ModelName)
        .HasMaxLength(DataSchemaConstants.DEFAULT_NAME_LENGTH)
        .IsRequired();
  }
}
