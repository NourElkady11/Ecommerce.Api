using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Basket_Item
    {
        public int Id { get; set; }

        public string? productName { get; set; }

        public string? pictureUrl { get; set; }

        public decimal? price { get; set; }
/*
        public string Type { get; set; }

        public string Brand { get; set; }*/

        public int? quantity { get; set; }




    }
}
