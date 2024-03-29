
namespace EmployeeDirectory.Concerns
{
    public interface IJsonServices
    {
        public List<Employee> GetEmployees();

        public bool UpdateEmployees(List<Employee> Employees);

        public List<Role> GetRoles();

        public bool UpdateRoles(List<Role> Roles);

    }
}