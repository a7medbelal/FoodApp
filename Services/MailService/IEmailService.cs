using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace FoodApp.Services.MailService
{
    public interface IEmailService
    {
        Task SendEmailAsync(string MailTo , string Subject , string Body ); 
    }
}
