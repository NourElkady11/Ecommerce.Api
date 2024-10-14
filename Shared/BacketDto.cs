using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public record BacketDto
    {
        public string Id { get; init; }

        public IEnumerable<Backet_ItemsDto?> Items { get; init; }
    }
}
