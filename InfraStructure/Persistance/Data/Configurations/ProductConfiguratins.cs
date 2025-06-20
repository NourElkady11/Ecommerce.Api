﻿using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistance.Data.Configurations
{
    public class ProductConfiguratins : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasOne(p => p.ProductBrand).WithMany(p => p.Products).HasForeignKey(p => p.brandId);
            builder.HasOne(p => p.productType).WithMany(p => p.Products).HasForeignKey(p => p.typeId);
            builder.Property(p => p.price).HasColumnType("decimal(18,3)");
           
        }
    }
}
