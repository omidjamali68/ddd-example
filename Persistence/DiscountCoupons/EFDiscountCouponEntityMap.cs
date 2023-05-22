using Domain.Aggregates.DiscountCoupons;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.DiscountCoupons
{
    public class EFDiscountCouponEntityMap : IEntityTypeConfiguration<DiscountCoupon>
    {
        public void Configure(EntityTypeBuilder<DiscountCoupon> builder)
        {
            builder.ToTable("DiscountCoupons");
            builder.HasKey(_ => _.Id);

            builder.Property(_ => _.Percent)
                .HasConversion(p => p.Value,
                    p => Domain.Aggregates.DiscountCoupons.ValueObjects.DiscountPercent.Create(p).Value);

            builder.Property(_ => _.ValidDateFrom)
                .HasConversion(f => f.Value,
                    f => Domain.Aggregates.DiscountCoupons.ValueObjects.ValidDateFrom.Create(f).Value);

            builder.Property(_ => _.ValidDateTo)
                .HasConversion(t => t.Value,
                    t => Domain.Aggregates.DiscountCoupons.ValueObjects.ValidDateTo.Create(t).Value);
        }
    }
}