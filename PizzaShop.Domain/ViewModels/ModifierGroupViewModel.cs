namespace PizzaShop.Domain.ViewModels;

public class ModifierGroupViewModel
{
     public int Groupid { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }

     public List<int>? ModifierIdss { get; set; }


     public string SelectedItemIds { get; set; } 
}