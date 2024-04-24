using System.Data;
using EmployeeDirectory.Concerns;
using EmployeeDirectory.Contracts;
namespace EmployeeDirectory.Services
{

    public class RoleService : IRoleService
    {

        private readonly IDatabaseServices databaseServices;

        public RoleService(IDatabaseServices databaseServices)
        {
            this.databaseServices = databaseServices;
        }

        public Role? GetById(string id)
        {
            try
            {
                return (from role in this.GetAll() where role.Id == id select role).SingleOrDefault();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public bool AreRolesExist()
        {
            return this.GetAll().Any();
        }

        public List<Role> GetAll()
        {
            DataTable dataTable = this.databaseServices.GetAll<Role>();
            List<Role> roles = new List<Role>();
            foreach (DataRow item in dataTable.Rows)
            {
                Role role = new Role()
                {
                    Id = item["Id"].ToString(),
                    Name = item["Role Name"].ToString(),
                    Location = item["Location"].ToString(),
                    Department = item["Department"].ToString(),
                    Description = item["Description"].ToString()
                };
                roles.Add(role);
            }
            return roles;
        }

        public List<string> GetProperty<T>()
        {
            List<string> propertyList = new List<string>();
            DataTable dataTable = databaseServices.GetMasterData<T>();
            foreach (DataRow item in dataTable.Rows)
                propertyList.Add(item["Id"].ToString() + " " + item["Name"].ToString());
            return propertyList;
        }

        public bool DeleteById(string Id)
        {
            try
            {
                if (!databaseServices.Delete<Role>(Id)) return false;
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public List<string> GetRoleName()
        {
            return (from role in this.GetAll() select role.Id + " " + role.Name?.ToUpper()).ToList();
        }

        public bool Update(string property, string value, int Id)
        {
            try
            {
                if (!databaseServices.Update<Role>(property, value, Id)) return false;
            }
            catch (System.Exception)
            {
                return false;
            }
            return true;
        }

        public bool Create(Role role)
        {
            try
            {
                if (!this.databaseServices.Create<Role>(role))
                    throw new Exception();

            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }


        public string GenerateId()
        {
            DateTime CurrentDate = DateTime.Now;
            return "TZ" + CurrentDate.Year + CurrentDate.Month + CurrentDate.Day + CurrentDate.Hour + CurrentDate.Minute + CurrentDate.Second;
        }
    }
}