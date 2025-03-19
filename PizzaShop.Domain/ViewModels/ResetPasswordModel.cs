using System.ComponentModel.DataAnnotations;

namespace PizzaShop.Domain.ViewModels;

public class ResetPasswordModel
{
    public string Token { get; set; }

     [DataType(DataType.Password)]
    [RegularExpression("([a-z]|[A-Z]|[0-9]|[\\W]){4}[a-zA-Z0-9\\W]{3,11}", ErrorMessage = "Password Must contain Special Symbol, Number,Alphabet")]
    public string NewPassword { get; set; }

     [DataType(DataType.Password)]
    [RegularExpression("([a-z]|[A-Z]|[0-9]|[\\W]){4}[a-zA-Z0-9\\W]{3,11}", ErrorMessage = "Password Must contain Special Symbol, Number,Alphabet")]
    public string ConfirmPassword { get; set; }
}
