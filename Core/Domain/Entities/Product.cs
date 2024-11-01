using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Product:BaseEntity<int>
    {
        public string? name { get; set; }
        public string? description { get; set; }
        public string? pictureUrl { get; set; }
        public decimal price { get; set; }

        public ProductBrand ProductBrand { get; set; }

        public int brandId { get; set; }

        public ProductType productType { get; set; }

        public int typeId { get; set; }

    }
}
