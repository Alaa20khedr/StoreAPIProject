using AutoMapper;
using StackExchange.Redis;
using Store.Data.Entities;
using Store.Data.Entities.OrderEntities;
using Store.Reposatory.Interfaces;
using Store.Reposatory.Specification.Order;
using Store.Service.OrderService.Dtos;
using Store.Service.PaymentService;
using Store.Service.Services.BasketService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Service.OrderService
{
    public class OrderService:IOrderService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IBasketService basketService;
        private readonly IMapper mapper;
        private readonly IPaymentService paymentService;

        public OrderService(IUnitOfWork unitOfWork , IBasketService basketService , IMapper mapper,IPaymentService paymentService)
        {
            this.unitOfWork = unitOfWork;
            this.basketService = basketService;
            this.mapper = mapper;
            this.paymentService = paymentService;
        }

        public async Task<OrderResultDto> CreateOrderAsync(OrderDto input)
        {
            var basket = await basketService.GetBasketAsync(input.BasketId);
            if (basket is null)
                throw new Exception("Basket Not Exist");
            var orderItems=new List<OrderItemDto>();
            foreach(var basketItem in  basket.basketItems) {
               var productItem=await unitOfWork.Repository<Product,int>().GetByIdAsync(basketItem.ProductId);
                if (productItem is null)
                    throw new Exception($"product with id:{basketItem.ProductId} Not Exist");
                var itemOrdered = new ProductItemOrdered
                {
                    ProductItemId = productItem.Id,
                    ProductName = productItem.Name,
                    PictureUrl = productItem.PictureUrl
                };
                var orderItem = new OrderItem
                {
                    Price = productItem.Price,
                    Quantity = basketItem.Quantity,
                    itemOrdered = itemOrdered

                };
                 var mappedOrderItem=mapper.Map<OrderItemDto>(orderItem);
                orderItems.Add(mappedOrderItem);
            
            }
            // Delivary Method
            var delivaryMethod=await unitOfWork.Repository<DelivaryMethod,int>().GetByIdAsync(input.DelivaryMethodId);
            if (delivaryMethod is null)
                throw new Exception("Delivary Method is not Provided");
            //calculate subtotal
            var subTotal = orderItems.Sum(item => item.Quantity * item.Price);
            //check if order exist
            var specs = new OrderWithPaymentSpecification(basket.PaymentIntentId);
            var exisitingOrder = await unitOfWork.Repository<Orders, Guid>().GetWithSpecificByIdAsync(specs);
            if(exisitingOrder != null)
            {
                unitOfWork.Repository<Orders, Guid>().Delete(exisitingOrder);
                await paymentService.CreateOrUpdatePaymentIntentForExistingOrder(basket);

            }
            else
                await paymentService.CreateOrUpdatePaymentINtentForNewOrder(basket.Id);
            //create order
            var mappedshippingAddress = mapper.Map<ShippingAddress>(input.ShippingAddress);
            var mappedorderitem = mapper.Map<List<OrderItem>>(orderItems);
            var order = new Orders
            {
                DelivaryMethodId=delivaryMethod.Id,
                ShippingAddress=mappedshippingAddress,
                BuyerEmail=input.BuyerEmail,
                OrderItems=mappedorderitem,
                SubTotal=subTotal,
                BasketId=basket.Id,
                PaymentIntentId=basket.PaymentIntentId
            };
            await unitOfWork.Repository<Orders,Guid>().AddAsync(order);
            await unitOfWork.CompleteAsync();
            var mappedOrder=mapper.Map<OrderResultDto>(order);
            mappedOrder.BasketId=basket.Id;
            return mappedOrder;
        }

        public async Task<IReadOnlyList<DelivaryMethod>> GetAllDelivaryMethodAsync()
        => await unitOfWork.Repository<DelivaryMethod, int>().GetAllAsync();

        public async Task<IReadOnlyList<OrderResultDto>> GetAllOrdersForUserAsync(string BuyerEmail)
        {
            var specs = new OrderWithSpcefication(BuyerEmail);
            var orders=await unitOfWork.Repository<Orders,Guid>().GetAllWithSpecificationAsync(specs);
            if (orders is { Count: <= 0 })
                throw new Exception("You Do Not Have Any Orders Yet");
            var mappedOrders=mapper.Map<List<OrderResultDto>>(orders);
            return mappedOrders;
        }

        public async Task<OrderResultDto> GetOrderByIdAsync(Guid Id, string BuyerEmail)
        {
            var specs = new OrderWithSpcefication(Id,BuyerEmail);
            var order = await unitOfWork.Repository<Orders, Guid>().GetWithSpecificByIdAsync(specs);
            if(order is null)
                throw new Exception($"there is no order with id :{Id}");
            var mappedOrder = mapper.Map<OrderResultDto>(order);
            return mappedOrder;
        }
    }
}
