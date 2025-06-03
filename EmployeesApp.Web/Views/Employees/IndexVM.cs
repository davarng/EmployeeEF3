namespace EmployeesApp.Web.Views.Employees
{
    public class IndexVM
    {
        public EmployeeVM[] EmployeeVMs { get; set; } = null!;

        public class EmployeeVM
        {
            public required int Id { get; set; }
            public required string Name { get; set; }
            public required bool ShowAsHighlighted { get; set; }
            public string? CompanyName { get; set; }
        }
    }
}
