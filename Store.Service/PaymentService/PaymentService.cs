using AutoMapper;
using Microsoft.Extensions.Configuration;
using Store.Data.Entities;
using Store.Data.Entities.OrderEntities;
using Store.Reposatory.Interfaces;
using Store.Reposatory.Specification.Order;
using Store.Service.OrderService.Dtos;
using Store.Service.Services.BasketService;
using Store.Service.Services.BasketService.DTOs;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Product = Store.Data.Entities.Product;

namespace Store.Service.PaymentService
{
    public class PaymentService : IPaymentService
    {
        private readonly IConfiguration configuration;
        private readonly IBasketService basketService;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public PaymentService(IConfiguration configuration, IBasketService basketService, IUnitOfWork unitOfWork , IMapper mapper)
        {
            this.configuration = configuration;
            this.basketService = basketService;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task<CustomerBasketDto> CreateOrUpdatePaymentIntentForExistingOrder(CustomerBasketDto input)
        {
            StripeConfiguration.ApiKey = configuration["Stripe:Secretkey"];
            if (input == null)
                throw new Exception("basket is null");


            var delivarymethod = await unitOfWork.Repository<DelivaryMethod, int>().GetByIdAsync(input.DelivaryMethodId.Value);
            var shippingprice = delivarymethod.Price;

            foreach (var item in input.basketItems)
            {
                var product = await unitOfWork.Repository<Product, int>().GetByIdAsync(item.ProductId);
                if (item.Price != product.Price)
                    item.Price = product.Price;
            }
            var service = new PaymentIntentService();
            PaymentIntent paymentIntent;
            if (string.IsNullOrEmpty(input.PaymentIntentId))
            {
                var options = new PaymentIntentCreateOptions
                {
                    Amount = (long)input.basketItems.Sum(item => item.Quantity * (item.Price * 100)) + (long)(shippingprice * 100),
                    Currency = "usd",
                    PaymentMethodTypes = new List<string> { "card" }

                };
                paymentIntent = await service.CreateAsync(options);
                input.PaymentIntentId = paymentIntent.Id;
                input.ClientSecret = paymentIntent.ClientSecret;
            }
            else
            {
                var options = new PaymentIntentUpdateOptions
                {
                    Amount = (long)input.basketItems.Sum(item => item.Quantity * (item.Price * 100)) + (long)(shippingprice * 100)
                };
                await service.UpdateAsync(input.PaymentIntentId, options);

            }
            await basketService.UpdateBasketAsync(input);
            return input;

        }

        public async Task<CustomerBasketDto> CreateOrUpdatePaymentINtentForNewOrder(string BasketId)
        {
            StripeConfiguration.ApiKey = configuration["Stripe:Secretkey"];
            var input = await basketService.GetBasketAsync(BasketId);
            if (input == null)
                throw new Exception("basket is null");


            var delivarymethod = await unitOfWork.Repository<DelivaryMethod, int>().GetByIdAsync(input.DelivaryMethodId.Value);
            var shippingprice = delivarymethod.Price;

            foreach (var item in input.basketItems)
            {
                var product = await unitOfWork.Repository<Product, int>().GetByIdAsync(item.ProductId);
                if (item.Price != product.Price)
                    item.Price = product.Price;
            }
            var service = new PaymentIntentService();
            PaymentIntent paymentIntent;
            if (string.IsNullOrEmpty(input.PaymentIntentId))
            {
                var options = new PaymentIntentCreateOptions
                {
                    Amount = (long)input.basketItems.Sum(item => item.Quantity * (item.Price * 100)) + (long)(shippingprice * 100),
                    Currency = "usd",
                    PaymentMethodTypes = new List<string> { "card" }

                };
                paymentIntent = await service.CreateAsync(options);
                input.PaymentIntentId = paymentIntent.Id;
                input.ClientSecret = paymentIntent.ClientSecret;
            }
            else
            {
                var options = new PaymentIntentUpdateOptions
                {
                    Amount = (long)input.basketItems.Sum(item => item.Quantity * (item.Price * 100)) + (long)(shippingprice * 100)
                };
                await service.UpdateAsync(input.PaymentIntentId, options);

            }
            await basketService.UpdateBasketAsync(input);
            return input;

        }



        public async Task<OrderResultDto> UpdateOrderPaymentFailed(string paymentIntentId)
        {
            var specs = new OrderWithPaymentSpecification(paymentIntentId);
            var order=await unitOfWork.Repository<Orders,Guid>().GetWithSpecificByIdAsync(specs);
            if(order is null)
                throw new Exception("Order Does not Exist");
            order.OrderPaymentStatus= OrderPaymentStatus.Failed;
            unitOfWork.Repository<Orders, Guid>().Update(order);
            await unitOfWork.CompleteAsync();
            var mappedorder=mapper.Map<OrderResultDto>(order);

            return mappedorder;
        }   

        public async Task<OrderResultDto> UpdateOrderPaymentSucceeded(string paymentIntentId)
        {
            var specs = new OrderWithPaymentSpecification(paymentIntentId);
            var order = await unitOfWork.Repository<Orders, Guid>().GetWithSpecificByIdAsync(specs);
            if (order is null)
                throw new Exception("Order Does not Exist");
            order.OrderPaymentStatus = OrderPaymentStatus.Failed;
            unitOfWork.Repository<Orders, Guid>().Update(order);
            await unitOfWork.CompleteAsync();
            await basketService.DeleteBasketAsync(order.BasketId);
            var mappedorder = mapper.Map<OrderResultDto>(order);

            return mappedorder;
        }
    }
}
