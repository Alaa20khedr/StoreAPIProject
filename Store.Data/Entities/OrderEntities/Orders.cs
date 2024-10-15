using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Data.Entities.OrderEntities
{
    public enum OrderPaymentStatus
    {
        Pending,
        Received,
        Failed

    }
    public class Orders:BaseEntity<Guid>
    {
        public string BuyerEmail {  get; set; }
        public DateTimeOffset   OrderDate { get; set; }= DateTimeOffset.Now;
        public ShippingAddress ShippingAddress { get; set; }
        public DelivaryMethod DelivaryMethod { get; set; }
        public int? DelivaryMethodId { get; set; }
        public OrderPaymentStatus OrderPaymentStatus { get; set; }= OrderPaymentStatus.Pending;
        public IReadOnlyList<OrderItem> OrderItems { get; set; } 
        public decimal SubTotal {  get; set; }
        public string? PaymentIntentId {  get; set; }
        public decimal GetTotal()
            => SubTotal + DelivaryMethod.Price;
        public string? BasketId { get; set; }
    }

  
}
