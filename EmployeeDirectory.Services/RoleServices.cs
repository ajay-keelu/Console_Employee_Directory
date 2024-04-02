using System.Text.Json;
using EmployeeDirectory.Concerns;
using EmployeeDirectory.Contracts;
namespace EmployeeDirectory.Services
{

    public class RoleService : IRoleService
    {

        private readonly IJsonServices jsonServices;

        public RoleService(IJsonServices jsonServices)
        {
            this.jsonServices = jsonServices;
        }

        public Role? GetById(string id)
        {
            try
            {
                return (from role in this.GetAll() where role.Id == id select role).Single();
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
            return (from role in this.jsonServices.GetAll<Role>() where role.IsActive select role).ToList<Role>();
        }

        public List<string> GetProperty<T>()
        {
            return this.jsonServices.GetMasterData<T>();

        }

        public bool DeleteById(string Id)
        {
            try
            {
                var Roles = new List<Role>();
                foreach (Role role in this.GetAll())
                {
                    if (role.Id == Id)
                        role.IsActive = false;

                    Roles.Add(role);
                }
                if (!jsonServices.Save<Role>(Roles)) throw new Exception();

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

        public bool Save(Role role)
        {
            if (string.IsNullOrEmpty(role.Id)) return this.Create(role);
            else return this.Update(role);
        }

        public bool Create(Role role)
        {
            try
            {
                role.Id = this.GenerateId();
                var Roles = this.jsonServices.GetAll<Role>();
                Roles.Add(role);

                if (!this.jsonServices.Save<Role>(Roles))
                    throw new Exception();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public bool Update(Role role)
        {
            try
            {
                var Roles = this.jsonServices.GetAll<Role>();
                int index = Roles.FindIndex(r => r.Id == role.Id);
                Roles[index] = role;

                if (!jsonServices.Save<Role>(Roles))
                    throw new Exception();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
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