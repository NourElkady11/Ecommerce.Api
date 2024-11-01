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
    internal class DeliveryMethodConf : IEntityTypeConfiguration<deliveryMethod>
    {
        public void Configure(EntityTypeBuilder<deliveryMethod> builder)
        {
            builder.Property(d => d.cost).HasColumnType("decimal(18,3)");
        }
    }
}
