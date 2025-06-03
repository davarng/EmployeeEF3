namespace EmployeesApp.Domain.Entities
{
    public class Company
    {
        public int Id { get; set; }
        public string CompanyName { get; set; } = null!;
        public string City { get; set; } = null!;
        public List<Employee> Employees { get; set; } = null!;
    }
}
