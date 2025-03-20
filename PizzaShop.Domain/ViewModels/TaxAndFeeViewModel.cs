using System.ComponentModel.DataAnnotations;

namespace PizzaShop.Domain.ViewModels;

public class TaxAndFeeViewModel
{
    public string? TaxId{get;set;}
    [Required]
    [MaxLength(20)]
    [RegularExpression(@"^\S+$", ErrorMessage = "No whiteSpaces are allowed")]
    public required string TaxName{get;set;}
    [Required]
    public required string Taxtype{get;set;}
    [Required]
    public required double TaxValue{get;set;}
    [Required]
    public required bool IsEnabled{get;set;}
    [Required]
    public required bool DefaultTax{get;set;}
    public string? CreatedBy {get;set;}
    public string? EditedBy{get;set;}
}
