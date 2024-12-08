using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class ProductSpecificationsParamters
    {
        public int? BrandId { get; set; }

        public int? TypeId { get; set; }

        public productFilterations? Sort { get; set; }


        public int PageIndex { get; set; } = 1;

        private int _pageSize=10;

        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = value > 20 ? 50 : value;
        }

        public string? Search { get; set; }

    }


    public enum productFilterations
    {
        nameAsc,
        name,
        priceAsc,
        priceDesc,
    }
}
