using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.DataBase.EntityConfiguration
{
    public class RegionTaxEntityConfiguration : IEntityTypeConfiguration<RegionTax>
    {
        public void Configure(EntityTypeBuilder<RegionTax> builder)
        {
            builder.Property(x => x.Id).IsRequired();
            builder.Property(x => x.DestinyRegionId).IsRequired();
            builder.Property(x => x.OriginRegionId).IsRequired();
            builder.Property(x => x.Tax).IsRequired();

            builder.HasOne(x => x.Origin).WithMany(x => x.IsOrigin).HasForeignKey(x => x.OriginRegionId).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(x => x.Destiny).WithMany().HasForeignKey(x => x.DestinyRegionId).OnDelete(DeleteBehavior.NoAction);
        }
    }
}