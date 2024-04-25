

using EmployeeDirectory.Concerns;

namespace EmployeeDirectory.Contracts
{
    public interface IEmployeeService
    {
        public List<Employee> GetAll();

        public Employee GetById(string id);

        public bool Update(string property, string value, int Id);

        public bool DeleteByID(string Id);

        public bool Create(Employee employee);

        public List<string> GetProperty<T>(string name) where T : new();

        public List<Employee> GetAssignedEmployees(string roleId);
    }
}