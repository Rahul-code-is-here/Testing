using System.ComponentModel.DataAnnotations;

namespace PizzaShop.Domain.ViewModels;

public class TableViewModel
{
    [Required]
    public required string SectionId{get;set;}
    public string? TableId{get;set;}
    [Required]
    [MaxLength(20)]
    [RegularExpression(@"^\S.*$", ErrorMessage = "The field cannot start with a whitespace.")]
    public required string TableName{get;set;}
    [Required]
    public required string  Capacity{get;set;}
    [Required]
    public required int status{get;set;}

    public string? CreatedBy{get;set;}
    public string? EditedBy{get;set;}
}
