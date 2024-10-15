using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Reposatory.BasketReposatory.Models
{
    public class CustomerBasket
    {
        public string Id { get; set; }
        public int? DelivaryMethodId {  get; set; }
        public decimal ShippingPrice { get; set; }
        public List<BasketItem> basketItems { get; set; }=new List<BasketItem>();
        public string? PaymentIntentId { get; set; }
        public string? ClientSecret { get; set; }

    }
}
