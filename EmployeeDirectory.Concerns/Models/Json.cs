namespace EmployeeDirectory.Concerns
{

    public class Database
    {
        public List<Employee>? Employees { get; set; }

        public List<Role>? Roles { get; set; }
    }
    public class SelectData
    {
        public List<string>? Locations { get; set; }
        public List<string>? Departments { get; set; }
        public List<string>? JobTitles { get; set; }
    }
}