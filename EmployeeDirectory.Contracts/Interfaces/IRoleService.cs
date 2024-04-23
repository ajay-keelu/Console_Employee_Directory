
using EmployeeDirectory.Concerns;

namespace EmployeeDirectory.Contracts
{
    public interface IRoleService
    {
        public List<Role> GetAll();

        public Role? GetById(string id);

        public bool Create(Role role);

        public bool DeleteById(string Id);

        public bool Update(string property, string value, int Id);

        public bool AreRolesExist();

        public List<string> GetProperty<T>();

        public List<string> GetRoleName();
    }
}