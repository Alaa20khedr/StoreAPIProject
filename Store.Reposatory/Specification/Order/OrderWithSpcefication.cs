using Store.Data.Entities.OrderEntities;
using Store.Reposatory.Specification.SpecifProducts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Reposatory.Specification.Order
{
    public class OrderWithSpcefication:BaseSpecification<Orders>
    {
        public OrderWithSpcefication(string buyerEmail) : base(order => order.BuyerEmail == buyerEmail)
        {
            AddInclude(order => order.OrderItems);
            AddInclude(order => order.DelivaryMethod);
            AddOrderByDesc(order => order.OrderDate);
        }
        public OrderWithSpcefication(Guid id,string buyerEmail) :
            base(order => order.BuyerEmail == buyerEmail &&order.Id==id )
        {
            AddInclude(order => order.OrderItems);
            AddInclude(order => order.DelivaryMethod);
           
        }
    }
}
