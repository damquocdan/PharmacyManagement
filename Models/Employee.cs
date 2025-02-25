using System;
using System.Collections.Generic;

namespace PharmacyManagement.Models;

public partial class Employee
{
    public int EmployeeId { get; set; }

    public string Name { get; set; } = null!;

    public string? Phone { get; set; }

    public string? Email { get; set; }

    public string? Position { get; set; }

    public decimal Salary { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
