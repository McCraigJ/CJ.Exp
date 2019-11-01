using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace CJ.Exp.API.Middleware
{
  public class ExceptionMiddleware
  {
    private readonly RequestDelegate _next;
    private readonly IHostingEnvironment _environment;
    private readonly ILogger _logger;

    public ExceptionMiddleware(RequestDelegate next, ILoggerFactory logger, IHostingEnvironment environment)
    {
      _logger = logger.CreateLogger("Api");
      _next = next;
      _environment = environment;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
      try
      {
        await _next(httpContext);
      }
      catch (Exception ex)
      {
        _logger.LogError($"Something went wrong: {ex}");
        await HandleExceptionAsync(httpContext, ex);
      }
    }

    private Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
      context.Response.ContentType = "application/json";
      context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

      return context.Response.WriteAsync(new ErrorDetails
      {
        StatusCode = context.Response.StatusCode,
        Message = exception is ExpApiException || _environment.IsDevelopment() 
          ? exception.Message 
          : "An error occurred processing your request"
      }.ToString());
    }

    private class ErrorDetails
    {
      public int StatusCode { get; set; }
      public string Message { get; set; }


      public override string ToString()
      {
        return JsonConvert.SerializeObject(this);
      }
    }
  }
}
