﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class OrderItemsDto
    {

        public int ProductsId { get; set; }
        public string ProductName { get; set; }
        public string PictureUrl { get; set; }

        public int Quantity { get; set; }
        public decimal Price { get; set; }

      
    }
}
