using System;
using System.Collections.Generic;

namespace PharmacyManagement.Models;

public partial class Cart
{
    public int CartId { get; set; }

    public int CustomerId { get; set; }

    public int MedicineId { get; set; }

    public int Quantity { get; set; }

    public decimal Price { get; set; }

    public DateTime AddedAt { get; set; }

    public virtual Customer Customer { get; set; } = null!;

    public virtual Medicine Medicine { get; set; } = null!;
}
