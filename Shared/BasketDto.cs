using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public record BasketDto
    {
        public string Id { get; init; }

        public IEnumerable<Basket_ItemsDto?> items { get; init; }

        public string? paymentIntentId { get; set; }

        public int? deliveryMethodId { get; set; }

        public string? clientSecret { get; set; }

        public decimal? shippingPrice { get; set; }
    }
}
