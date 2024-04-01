using System.Text.Json;
using EmployeeDirectory.Concerns;
using EmployeeDirectory.Contracts;

namespace EmployeeDirectory.Services
{

    public class JsonServices : IJsonServices
    {
        public readonly string db = @"../EmployeeDirectory.Database/db.json";

        public List<T> GetAll<T>()
        {
            List<T> list = new List<T>();
            try
            {
                Database jsonData = JsonSerializer.Deserialize<Database>(File.ReadAllText(this.db));

                if (typeof(T) == typeof(Employee))
                {
                    list = jsonData.Employees.Cast<T>().ToList();
                }
                else
                {
                    list = jsonData.Roles.Cast<T>().ToList();
                }
            }
            catch (System.Exception)
            {
                list = this.GetAll<T>();
            }
            return list;
        }

        public bool Save<T>(List<T> list)
        {
            try
            {
                Database jsonData = JsonSerializer.Deserialize<Database>(File.ReadAllText(this.db));
                if (typeof(T) == typeof(Employee))
                {
                    jsonData.Employees = list.Cast<Employee>().ToList();
                }
                else
                {
                    jsonData.Roles = list.Cast<Role>().ToList();
                }
                File.WriteAllText(db, JsonSerializer.Serialize(jsonData));
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}