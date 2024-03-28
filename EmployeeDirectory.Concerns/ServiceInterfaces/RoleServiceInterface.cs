
namespace EmployeeDirectory.Concerns
{
    public interface IRoleService
    {
        public Role? GetById(string id);

        public bool AreRolesExist();

        public List<Role> GetAll();

        public bool DeleteById(string Id);

        public List<string> GetRoleName();

        public bool Save(Role role);

        public bool Create(Role role);

        public bool Update(Role role);

        public string GenerateId();


    }
}