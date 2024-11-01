using Domain.Entities.OrderEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistance.Data.Configurations
{
    internal class OrderConfigurations : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasMany(e=>e.orderItems).WithOne().OnDelete(DeleteBehavior.Cascade);

            builder.OwnsOne(e => e.ShippingAddress, e => e.WithOwner());

            builder.HasOne(e => e.DeliveryWay).WithOne(e => e.Order).HasForeignKey<Order>(e=>e.DelierywaysID).OnDelete(DeleteBehavior.SetNull);

            builder.Property(d => d.Subtotal).HasColumnType("decimal(18,3)");

            builder.Property(e => e.PymentStatus).HasConversion(e => e.ToString(), e => Enum.Parse<OrderPymentStatus>(e));
            //Stores the value of the label not int value so convert from enum to string
            //the second paramter is to convert from string to enum

        }
    }
}
