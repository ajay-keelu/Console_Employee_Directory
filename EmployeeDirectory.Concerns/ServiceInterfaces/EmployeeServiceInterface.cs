
namespace EmployeeDirectory.Concerns
{
    public interface IEmployeeService
    {
        public Employee GetById(string id);

        public List<Employee> GetAssignedEmployee(string Id);

        public List<Employee> GetAll();

        public bool Create(Employee employee);

        public bool Update(Employee employee);

        public bool DeleteByID(string Id);

        public bool Save(Employee employee);

        public string GenerateId();
    }
}