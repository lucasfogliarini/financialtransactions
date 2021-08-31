using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.ComponentModel.DataAnnotations;

namespace FinancialTransactions.Api
{
    public class RequestMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public RequestMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            _next = next;
            _logger = loggerFactory.CreateLogger<RequestMiddleware>();
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (ValidationException ex)
            {
                var problemDetails = ValidationProblem(ex.Message, StatusCodes.Status400BadRequest);
                httpContext.Response.StatusCode = problemDetails.Status.Value;
                await httpContext.Response.WriteAsJsonAsync(problemDetails);
            }
            catch (Exception ex)
            {
                var title = "Ocorreu um problema na aplicação.";
                var problemDetails = ValidationProblem(title, StatusCodes.Status500InternalServerError, ex.Message);
                httpContext.Response.StatusCode = problemDetails.Status.Value;
                await httpContext.Response.WriteAsJsonAsync(problemDetails);
            }
            finally
            {
                var userEmail = httpContext.User.FindFirst(ClaimTypes.Email)?.Value ?? "Unknown";
                var userId = httpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "?";
                _logger.LogInformation(
                    "{userEmail}({userId}) {method} {path}{queryString} => {statusCode}",
                    userEmail,
                    userId,
                    httpContext.Request?.Method,
                    httpContext.Request?.Path.Value,
                    httpContext.Request?.QueryString,
                    httpContext.Response?.StatusCode);
            }
        }

        private ValidationProblemDetails ValidationProblem(string title, int statusCode, string detail = null)
        {
            var problemDetails = new ValidationProblemDetails
            {
                Title = title,
                Detail = detail ?? title,
                Status = statusCode
            };
            return problemDetails;
        }
    }
}