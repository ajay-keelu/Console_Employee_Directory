
using EmployeeDirectory.Concerns;

namespace EmployeeDirectory.Contracts
{
    public interface IEmployeeService
    {
        public List<Employee> GetAll();

        public Employee GetById(string id);

        public bool Save(Employee employee);

        public bool DeleteByID(string Id);

        public List<string> GetProperty<T>();

        public List<Employee> GetAssignedEmployees(string roleId);
    }
}