namespace PizzaShop.Domain.ViewModels;

public class ModifiersViewModel
{
    public int Id { get; set; }

    public int Modifiergroupid { get; set; }

    public string Name { get; set; } = null!;

    public decimal Price { get; set; }

    public string Unittype { get; set; } = null!;

    public int Quantity { get; set; }

    public string? Description { get; set; }

    public bool Isdeleted { get; set; }

    public List<int> ModifierGroupIds { get; set; } = new List<int>();  // Updated to List



}
