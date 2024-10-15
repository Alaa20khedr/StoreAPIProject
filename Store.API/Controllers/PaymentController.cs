using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Store.Service.OrderService.Dtos;
using Store.Service.PaymentService;
using Store.Service.Services.BasketService.DTOs;
using Stripe;

namespace Store.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PaymentController : ControllerBase

    {
        private readonly IPaymentService paymentService;
        private readonly ILogger<PaymentController> logger;
        private const string endpointSecret = "whsec_1dbc6fcba749a7096a230c446a012d64143166675634e9e97051143931563b53";

        public PaymentController(IPaymentService paymentService, ILogger<PaymentController> logger)
        {
            this.paymentService = paymentService;
            this.logger = logger;
        }
        [HttpPost("{basketId}")]
        public async Task<ActionResult<CustomerBasketDto>> CreateOrUpdatePaymentIntentForExistingOrder(CustomerBasketDto input)
        => Ok(await paymentService.CreateOrUpdatePaymentIntentForExistingOrder(input));
        [HttpPost("{basketId}")]
        public async Task<ActionResult<CustomerBasketDto>> CreateOrUpdatePaymentINtentForNewOrder(string  BasketId)
              => Ok(await paymentService.CreateOrUpdatePaymentINtentForNewOrder(BasketId));

        
          

            [HttpPost("webhook")]
            public async Task<IActionResult> Index()
            {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
            try
            {
                var stripeEvent = EventUtility.ConstructEvent(json,
                    Request.Headers["Stripe-Signature"], endpointSecret);
                PaymentIntent paymentIntent;
                OrderResultDto orderResultDto;
                // Handle the event
                if (stripeEvent.Type == Events.PaymentIntentPaymentFailed)
                {
                    paymentIntent = (PaymentIntent)stripeEvent.Data.Object;
                    logger.LogInformation("paymentfailed:", paymentIntent.Id);
                    orderResultDto = await paymentService.UpdateOrderPaymentFailed(paymentIntent.Id);
                    logger.LogInformation("order updated to payment failed:", orderResultDto.BasketId);
                }
                else if (stripeEvent.Type == Events.PaymentIntentSucceeded)
                {
                    paymentIntent = (PaymentIntent)stripeEvent.Data.Object;
                    logger.LogInformation("paymentsucced:", paymentIntent.Id);
                    orderResultDto = await paymentService.UpdateOrderPaymentSucceeded(paymentIntent.Id);
            logger.LogInformation("order updated to payment succeed:", orderResultDto.BasketId);
        }
                // ... handle other event types
                else
                {
                    Console.WriteLine("Unhandled event type: {0}", stripeEvent.Type);
                }

                return Ok();
        }
            catch (StripeException e)
            {
                return BadRequest();
    }
}
        }
    
}
