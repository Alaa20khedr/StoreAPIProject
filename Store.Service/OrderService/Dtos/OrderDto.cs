﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Service.OrderService.Dtos
{
    public class OrderDto
    {
        public string BasketId { get; set; }
        public string BuyerEmail { get; set;}
        [Required]
        public int DelivaryMethodId { get; set; }
        public AddressDto ShippingAddress {  get; set; }
    }
}
