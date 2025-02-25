using System;
using System.Collections.Generic;

namespace PharmacyManagement.Models;

public partial class OrderDetail
{
    public int OrderDetailId { get; set; }

    public int OrderId { get; set; }

    public int MedicineId { get; set; }

    public int Quantity { get; set; }

    public decimal Price { get; set; }

    public virtual Medicine Medicine { get; set; } = null!;

    public virtual Order Order { get; set; } = null!;
}
