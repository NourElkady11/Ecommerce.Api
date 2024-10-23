using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exeption
{
    public class DeliveryWaysNotFound(int id):Exception($"The Delivery with the id {id} is Not Found")
    {
    }
}
