﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public record PaginatedResult<Tdata>(int PageIndex,int PageSize,int TotalCount,IEnumerable<Tdata> Data)
    {
    }
}