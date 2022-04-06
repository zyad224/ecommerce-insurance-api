using Insurance.Domain.DomainExceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace Insurance.Api.ExceptionHandlerMiddleware
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception error)
            {
                var response = context.Response;
                response.ContentType = "application/json";

                if ((error.GetType() == typeof(InvalidInsuranceException)) ||
                   (error.GetType() == typeof(InvalidProductException)) ||
                   (error.GetType() == typeof(InvalidSurchargeException)))
                    response.StatusCode = (int)HttpStatusCode.BadRequest;

                else if ((error.GetType() == typeof(KeyNotFoundException)) ||
                  (error.GetType() == typeof(NotFoundSurchargeException)))
                    response.StatusCode = (int)HttpStatusCode.NotFound;

                else if (error.GetType() == typeof(DbUpdateConcurrencyException))
                     response.StatusCode = (int)HttpStatusCode.Conflict;

                else
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;

                var result = JsonSerializer.Serialize(new { message = error?.Message });
                await response.WriteAsync(result);
            }
        }
    }
}
