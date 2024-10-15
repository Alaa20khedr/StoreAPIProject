using Store.Reposatory.BasketReposatory.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Service.Services.BasketService.DTOs
{
    public class CustomerBasketDto
    {
        public string Id { get; set; }
        public int? DelivaryMethodId { get; set; }
        public decimal ShippingPrice { get; set; }
        public List<BasketItemDTO> basketItems { get; set; } = new List<BasketItemDTO>();
        public string? PaymentIntentId { get; set; }
        public string? ClientSecret { get; set; }
    }
}
