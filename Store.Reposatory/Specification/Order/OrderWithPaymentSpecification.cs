using Store.Data.Entities.OrderEntities;
using Store.Reposatory.Specification.SpecifProducts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Reposatory.Specification.Order
{
    public class OrderWithPaymentSpecification:BaseSpecification<Orders>
    {
        public OrderWithPaymentSpecification(string? paymentIntentId) :base(order=>order.PaymentIntentId==paymentIntentId) { }

    }
}
