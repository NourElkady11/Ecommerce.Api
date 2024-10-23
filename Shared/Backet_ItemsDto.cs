using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public record Backet_ItemsDto
    {
        public int Id { get; init; }

        public string Product_Name { get; init; }

        public string PictureUrl { get; init; }

        [Range(1,double.MaxValue)]
        public decimal Price { get; init; }

        public string Type { get; init; }

        public string Brand { get; init; }

        [Range(1,100)]
        public int Quantity { get; init; }

    }
}
