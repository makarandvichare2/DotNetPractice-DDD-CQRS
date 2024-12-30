using BikeShop.Core.Bikes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BikeShop.Infrastructure.Data.Config;

public class BikeConfiguration : IEntityTypeConfiguration<Bike>
{
  public void Configure(EntityTypeBuilder<Bike> builder)
  {
        builder.OwnsOne(p => p.Price, p =>
        {
            p.Property(pp => pp.Currency)
            .HasColumnName("Price_Currency");
            p.Property(pp => pp.PriceValue)
            .HasColumnName("Price_PriceValue");
        });
        builder.OwnsOne(p => p.Weight, p =>
        {
            p.Property(pp => pp.Unit)
            .HasColumnName("Weight_Unit");
            p.Property(pp => pp.WeightValue)
            .HasColumnName("Weight_WeightValue");
        });
        builder.OwnsOne(p => p.Colour, p =>
        {
            p.Property(pp => pp.ColourCode)
            .HasColumnName("ColourCode");
        });
    }
}
