using System;
using System.Collections.Generic;

namespace PharmacyManagement.Models;

public partial class Order
{
    public int OrderId { get; set; }

    public int CustomerId { get; set; }

    public int? EmployeeId { get; set; }

    public DateTime OrderDate { get; set; }

    public string Status { get; set; } = null!;

    public decimal TotalAmount { get; set; }

    public virtual Customer Customer { get; set; } = null!;

    public virtual Employee? Employee { get; set; }

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
}
