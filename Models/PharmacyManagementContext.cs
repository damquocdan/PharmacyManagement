using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace PharmacyManagement.Models;

public partial class PharmacyManagementContext : DbContext
{
    public PharmacyManagementContext()
    {
    }

    public PharmacyManagementContext(DbContextOptions<PharmacyManagementContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Admin> Admins { get; set; }

    public virtual DbSet<Cart> Carts { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Import> Imports { get; set; }

    public virtual DbSet<Medicine> Medicines { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderDetail> OrderDetails { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DAMQUOCDAN;Database=PharmacyManagement;uid=sa;pwd=1234;MultipleActiveResultSets=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Admin>(entity =>
        {
            entity.HasKey(e => e.AdminId).HasName("PK__Admins__AD0500A6B758552E");

            entity.HasIndex(e => e.Username, "UQ__Admins__F3DBC572604CB0A0").IsUnique();

            entity.Property(e => e.AdminId).HasColumnName("adminId");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("createdAt");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .HasColumnName("password");
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .HasColumnName("phone");
            entity.Property(e => e.Role)
                .HasMaxLength(50)
                .HasDefaultValue("Admin")
                .HasColumnName("role");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updatedAt");
            entity.Property(e => e.Username)
                .HasMaxLength(255)
                .HasColumnName("username");
        });

        modelBuilder.Entity<Cart>(entity =>
        {
            entity.HasKey(e => e.CartId).HasName("PK__Cart__415B03B8D12F6A71");

            entity.ToTable("Cart");

            entity.Property(e => e.CartId).HasColumnName("cartId");
            entity.Property(e => e.AddedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("addedAt");
            entity.Property(e => e.CustomerId).HasColumnName("customerId");
            entity.Property(e => e.MedicineId).HasColumnName("medicineId");
            entity.Property(e => e.Price)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("price");
            entity.Property(e => e.Quantity).HasColumnName("quantity");

            entity.HasOne(d => d.Customer).WithMany(p => p.Carts)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Cart__customerId__5070F446");

            entity.HasOne(d => d.Medicine).WithMany(p => p.Carts)
                .HasForeignKey(d => d.MedicineId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Cart__medicineId__4F7CD00D");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK__Category__23CAF1D85E333D50");

            entity.ToTable("Category");

            entity.Property(e => e.CategoryId).HasColumnName("categoryId");
            entity.Property(e => e.CategoryName)
                .HasMaxLength(255)
                .HasColumnName("categoryName");
            entity.Property(e => e.Description)
                .HasMaxLength(1000)
                .HasColumnName("description");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.CustomerId).HasName("PK__Customer__B611CB7DCDD702E1");

            entity.Property(e => e.CustomerId).HasColumnName("customerId");
            entity.Property(e => e.Address)
                .HasMaxLength(500)
                .HasColumnName("address");
            entity.Property(e => e.Birthday).HasColumnName("birthday");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .HasColumnName("password");
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .HasColumnName("phone");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.EmployeeId).HasName("PK__Employee__C134C9C1E9391DE2");

            entity.Property(e => e.EmployeeId).HasColumnName("employeeId");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .HasColumnName("phone");
            entity.Property(e => e.Position)
                .HasMaxLength(255)
                .HasColumnName("position");
            entity.Property(e => e.Salary)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("salary");
        });

        modelBuilder.Entity<Import>(entity =>
        {
            entity.HasKey(e => e.ImportId).HasName("PK__Imports__2CC5AB67EEA4619A");

            entity.Property(e => e.ImportId).HasColumnName("importId");
            entity.Property(e => e.CostPrice)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("costPrice");
            entity.Property(e => e.ImportDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("importDate");
            entity.Property(e => e.MedicineId).HasColumnName("medicineId");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.SupplierName)
                .HasMaxLength(255)
                .HasColumnName("supplierName");

            entity.HasOne(d => d.Medicine).WithMany(p => p.Imports)
                .HasForeignKey(d => d.MedicineId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Imports__medicin__5535A963");
        });

        modelBuilder.Entity<Medicine>(entity =>
        {
            entity.HasKey(e => e.MedicineId).HasName("PK__Medicine__BA9E65EEC72AD2B3");

            entity.Property(e => e.MedicineId).HasColumnName("medicineId");
            entity.Property(e => e.CategoryId).HasColumnName("categoryId");
            entity.Property(e => e.CostPrice)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("costPrice");
            entity.Property(e => e.Description)
                .HasMaxLength(1000)
                .HasColumnName("description");
            entity.Property(e => e.ExpiryDate).HasColumnName("expiryDate");
            entity.Property(e => e.Image).HasColumnName("image");
            entity.Property(e => e.Manufacturer)
                .HasMaxLength(255)
                .HasColumnName("manufacturer");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.Price)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("price");
            entity.Property(e => e.Stock).HasColumnName("stock");
            entity.Property(e => e.Usage)
                .HasMaxLength(500)
                .HasColumnName("usage");

            entity.HasOne(d => d.Category).WithMany(p => p.Medicines)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK__Medicines__categ__4E88ABD4");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PK__Orders__0809335D428B0D38");

            entity.Property(e => e.OrderId).HasColumnName("orderId");
            entity.Property(e => e.CustomerId).HasColumnName("customerId");
            entity.Property(e => e.EmployeeId).HasColumnName("employeeId");
            entity.Property(e => e.OrderDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("orderDate");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasDefaultValue("Processing")
                .HasColumnName("status");
            entity.Property(e => e.TotalAmount)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("totalAmount");

            entity.HasOne(d => d.Customer).WithMany(p => p.Orders)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Orders__customer__5165187F");

            entity.HasOne(d => d.Employee).WithMany(p => p.Orders)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("FK__Orders__employee__52593CB8");
        });

        modelBuilder.Entity<OrderDetail>(entity =>
        {
            entity.HasKey(e => e.OrderDetailId).HasName("PK__OrderDet__E4FEDE4ABDB4E479");

            entity.Property(e => e.OrderDetailId).HasColumnName("orderDetailId");
            entity.Property(e => e.MedicineId).HasColumnName("medicineId");
            entity.Property(e => e.OrderId).HasColumnName("orderId");
            entity.Property(e => e.Price)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("price");
            entity.Property(e => e.Quantity).HasColumnName("quantity");

            entity.HasOne(d => d.Medicine).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.MedicineId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__OrderDeta__medic__5441852A");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__OrderDeta__order__534D60F1");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
