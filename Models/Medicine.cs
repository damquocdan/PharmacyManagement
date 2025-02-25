using System;
using System.Collections.Generic;

namespace PharmacyManagement.Models;

public partial class Medicine
{
    public int MedicineId { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public string? Manufacturer { get; set; }

    public decimal Price { get; set; }

    public decimal CostPrice { get; set; }

    public int Stock { get; set; }

    public DateOnly ExpiryDate { get; set; }

    public string? Usage { get; set; }

    public int? CategoryId { get; set; }

    public byte[]? Image { get; set; }

    public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();

    public virtual Category? Category { get; set; }

    public virtual ICollection<Import> Imports { get; set; } = new List<Import>();

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
}
