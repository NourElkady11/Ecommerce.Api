using Domain.Contracts;
using Domain.Entities.OrderEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Services.Specfications
{
    public class OrderWithIncludeSpecficarions : Specifications<Order>
    {
        public OrderWithIncludeSpecficarions(Guid id) : base(o=>o.Id==id)
        {
            AddInclude(o => o.orderItems);
            AddInclude(o => o.DeliveryWay);

        }


        public OrderWithIncludeSpecficarions(string email) : base(o => o.UserEmail == email)
        {
            AddInclude(o => o.orderItems);
            AddInclude(o => o.DeliveryWay);
            SetOrderBy(o => o.OrderDate);
        }


    }
}
