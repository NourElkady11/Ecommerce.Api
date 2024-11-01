using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.OrderEntities
{
    public class deliveryMethod:BaseEntity<int>
    {
        public deliveryMethod()
        {
            
        }
        public deliveryMethod(string shortName, string description, string deliveryTime, decimal cost)
        {
            shortName = shortName;
            description = description;
            deliveryTime = deliveryTime;
            cost = cost;
        }

        public string shortName { get; set; }
        public string description { get; set; }
        public string deliveryTime { get; set; }
        public decimal cost { get; set; }

        public Order Order { get; set; }
      
    }
}
