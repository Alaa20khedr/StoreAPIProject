﻿using Newtonsoft.Json.Serialization;
using Store.Service.HandleResponses;
using System.Net;
using System.Text.Json;

namespace Store.API.MiddleWares
{
    public class ExceptionMiddleWare
    {
        private readonly RequestDelegate next;
        private readonly IHostEnvironment environment;
        private readonly ILogger<ExceptionMiddleWare> logger;

        public ExceptionMiddleWare(RequestDelegate next,IHostEnvironment environment,ILogger<ExceptionMiddleWare> logger)
        {
            this.next = next;
            this.environment = environment;
            this.logger = logger;
        }
        public async   Task InvokeAsync(HttpContext context)
        {
            try
            {
              await next(context);
            }
            catch (Exception ex)
            {
                logger.LogError(ex,ex.Message);
                context.Response.ContentType = "application/json";
                context.Response.StatusCode=(int) HttpStatusCode.InternalServerError;
                var response = environment.IsDevelopment() 
                   ? new CustomException((int)HttpStatusCode.InternalServerError, ex.Message, ex.StackTrace) 
                    :new CustomException((int)HttpStatusCode.InternalServerError);

                var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
                var json=JsonSerializer.Serialize(response, options);
                await context.Response.WriteAsync(json);


            }

        }
    }
}
