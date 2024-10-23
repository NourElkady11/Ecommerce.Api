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
    internal class DeliveryMethodConf : IEntityTypeConfiguration<DeliveryWays>
    {
        public void Configure(EntityTypeBuilder<DeliveryWays> builder)
        {
            builder.Property(d => d.Cost).HasColumnType("decimal(18,3)");
        }
    }
}
