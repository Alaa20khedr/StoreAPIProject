using Store.Service.OrderService.Dtos;
using Store.Service.Services.BasketService.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Service.PaymentService
{
    public interface IPaymentService
    {
        Task<CustomerBasketDto> CreateOrUpdatePaymentIntentForExistingOrder(CustomerBasketDto input);
        Task<CustomerBasketDto> CreateOrUpdatePaymentINtentForNewOrder(string BasketId);
        Task<OrderResultDto> UpdateOrderPaymentSucceeded(string paymentIntentId);
        Task<OrderResultDto> UpdateOrderPaymentFailed(string paymentIntentId);
    }
}
