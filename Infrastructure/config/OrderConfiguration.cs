using Core.Entities.OrderAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.config;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        //owner owns entity
        builder.OwnsOne(x=>x.ShippingAddress,o=>o.WithOwner());

        //owner owns entity
        builder.OwnsOne(x=>x.PaymentSummary,o=>o.WithOwner());

        //convert status enum to strin
        builder.Property(x=>x.Status).HasConversion(
            o=>o.ToString(),
            o=>(OrderStatus)Enum.Parse(typeof(OrderStatus),o)
        );
        builder.Property(x=>x.SubTotal).HasColumnType("decimal(18,2)");

        //if we delete order , it will delete related items with order
        builder.HasMany(x=>x.OrderItems).WithOne().OnDelete(DeleteBehavior.Cascade);

        //convert datetime into genuine utc
        builder.Property(x=>x.OrderDate).HasConversion(
            d=>d.ToUniversalTime(),
            d=>DateTime.SpecifyKind(d,DateTimeKind.Utc)
        );
    }
}
