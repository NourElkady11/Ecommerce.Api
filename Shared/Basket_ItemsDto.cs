using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public record Basket_ItemsDto
    {
        public int id { get; init; }

        public string? productName { get; init; }

        public string? pictureUrl { get; init; }

      /*  [Range(1,double.MaxValue)]*/
        public decimal Price { get; init; }

        [Range(1,100)]
        public int quantity { get; init; }

    }
}
