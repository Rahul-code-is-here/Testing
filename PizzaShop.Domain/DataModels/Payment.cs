using System;
using System.Collections.Generic;

namespace PizzaShop.Domain.DataModels;

public partial class Payment
{
    public int Id { get; set; }

    public int OrderId { get; set; }

    public decimal? Amount { get; set; }

    public int? PaymentMethod { get; set; }

    public string Status { get; set; } = null!;

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public int? CreatedBy { get; set; }

    public int? ModifiedBy { get; set; }

    public virtual User? CreatedByNavigation { get; set; }

    public virtual User? ModifiedByNavigation { get; set; }

    public virtual Order Order { get; set; } = null!;
}
