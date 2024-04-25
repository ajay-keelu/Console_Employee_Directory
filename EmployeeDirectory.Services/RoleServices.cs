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
            return this.databaseServices.GetAll<Role>("Role");
        }

        public List<string> GetProperty<T>(string name) where T : new()
        {
            List<T> propertyList = databaseServices.GetAll<T>(name);
            List<string> res = new List<string>();
            if (typeof(T) == typeof(MasterData))
            {
                foreach (T item in propertyList)
                {
                    MasterData md = (MasterData)(object)item;
                    res.Add(md.Id.ToString() + " " + md.Name.ToString());
                }
            }
            return res;
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