
using FoodApp.Core.Enums;
using FoodApp.Core.ViewModle;
using FoodApp.Core.ViewModle.authVIewModel;

namespace FoodApp.Core.Helper
{
    public class GlobalErrorHandling
    {
      
            RequestDelegate next;

            public GlobalErrorHandling(RequestDelegate _next)
            {
                next = _next;
            }

            public async Task InvokeAsync(HttpContext context)
            {

                try
                {  
                    await next(context);
                }
                catch (Exception ex)
                {
                    File.WriteAllText(@"F:\\log.txt", $"error{ex.Message}");

                    var Response = new FailerResView<bool>(Errorcode.None);

                    await context.Response.WriteAsJsonAsync(Response);
                }


            }


        }
    }
