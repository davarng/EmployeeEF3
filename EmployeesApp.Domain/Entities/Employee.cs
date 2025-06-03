namespace EmployeesApp.Domain.Entities
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public decimal Salary { get; set; } = 0m; // Default
        public decimal Bonus { get; set; } = 0m;
        public int? CompanyId { get; set; } // Nullable and...
        public Company? Company { get; set; } // ... nullable
    }
}