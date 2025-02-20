﻿using Store.Data.Entities;
using Store.Service.OrderService.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Service.OrderService
{
    public interface IOrderService
    {
        Task<OrderResultDto> CreateOrderAsync(OrderDto input);
        Task<IReadOnlyList<OrderResultDto>> GetAllOrdersForUserAsync(string BuyerEmail);
        Task<OrderResultDto> GetOrderByIdAsync(Guid Id,string BuyerEmail);
        Task<IReadOnlyList<DelivaryMethod>> GetAllDelivaryMethodAsync();
    }
}
