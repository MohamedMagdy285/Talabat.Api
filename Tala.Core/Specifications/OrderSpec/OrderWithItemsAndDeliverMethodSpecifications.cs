using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tala.Core.Entities.Order_Aggregate;

namespace Tala.Core.Specifications.OrderSpec
{
    public class OrderWithItemsAndDeliverMethodSpecifications : BaseSpecification<Order>
    {
        public OrderWithItemsAndDeliverMethodSpecifications(string email)
            :base(o => o.BuyerEmail == email)
        {
            Include.Add(o => o.DeliveryMethod);
            Include.Add(o => o.Items);
            AddOrderBy(o => o.OrderDate);
        }
        
    }
}
