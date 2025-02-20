﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Store.Service.Services.BasketService;
using Store.Service.Services.BasketService.DTOs;

namespace Store.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly IBasketService basketService;

        public BasketController(IBasketService basketService)
        {
            this.basketService = basketService;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerBasketDto>> GetBasketById(string id)
            =>Ok(await basketService.GetBasketAsync(id));
        [HttpPost]
        public async Task<ActionResult<CustomerBasketDto>> UpdateBasketAsync(CustomerBasketDto basket)
            =>Ok(await basketService.UpdateBasketAsync(basket));
        [HttpDelete]
        public async Task<ActionResult> DeleteBasketAsync(string id)
            =>Ok(await basketService.DeleteBasketAsync(id));
    }
}
