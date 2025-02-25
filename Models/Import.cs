using System;
using System.Collections.Generic;

namespace PharmacyManagement.Models;

public partial class Import
{
    public int ImportId { get; set; }

    public string SupplierName { get; set; } = null!;

    public DateTime ImportDate { get; set; }

    public int MedicineId { get; set; }

    public int Quantity { get; set; }

    public decimal CostPrice { get; set; }

    public virtual Medicine Medicine { get; set; } = null!;
}
