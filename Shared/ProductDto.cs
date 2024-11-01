using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public record ProductDto
    {
        public int Id { get; set; }

        public string? name { get; set; }

        public string? description { get; set; }

        public string? pictureUrl { get; set; }

        public decimal Price { get; set; }

        public string brandName { get; set; }
        public string typeName { get; set; }
    }
}
