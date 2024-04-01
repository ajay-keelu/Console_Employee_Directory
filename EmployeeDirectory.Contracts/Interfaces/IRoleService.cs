
using EmployeeDirectory.Concerns;

namespace EmployeeDirectory.Contracts
{
    public interface IRoleService
    {
        public List<Role> GetAll();

        public Role? GetById(string id);

        public bool Save(Role role);

        public bool DeleteById(string Id);

        public bool AreRolesExist();

        public List<string> GetProperty(string prop);

        public List<string> GetRoleName();
    }
}