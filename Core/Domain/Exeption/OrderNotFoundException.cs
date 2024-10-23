﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exeption
{
    public class OrderNotFoundException(Guid id):Exception($"Order with id {id} is Not Found")
    {

    }
}
