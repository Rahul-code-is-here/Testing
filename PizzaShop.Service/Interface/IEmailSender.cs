namespace PizzaShop.Service.Interface;

public interface IEmailSender
{
    Task<bool> SendEmailAsync(string email, string subject, string htmlMessage);
}

