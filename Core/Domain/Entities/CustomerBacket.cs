using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class CustomerBacket
    {
        public string Id { get; set; }

        public IEnumerable<Backet_Item?> Items { get; set; }
    }
}
