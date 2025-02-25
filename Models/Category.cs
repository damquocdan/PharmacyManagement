using System;
using System.Collections.Generic;

namespace PharmacyManagement.Models;

public partial class Category
{
    public int CategoryId { get; set; }

    public string CategoryName { get; set; } = null!;

    public string? Description { get; set; }

    public virtual ICollection<Medicine> Medicines { get; set; } = new List<Medicine>();
}
