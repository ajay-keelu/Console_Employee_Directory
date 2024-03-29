using System.Text.Json;
using EmployeeDirectory.Concerns;

namespace EmployeeDirectory.Services
{

    public class JsonServices : IJsonServices
    {
        public readonly string db = @"../EmployeeDirectory.Database/db.json";

        public List<Employee> GetEmployees()
        {

            return JsonSerializer.Deserialize<JsonData>(File.ReadAllText(this.db))?.Employees;
        }

        public bool UpdateEmployees(List<Employee> Employees)
        {
            try
            {
                JsonData jsonData = JsonSerializer.Deserialize<JsonData>(File.ReadAllText(this.db));
                jsonData.Employees = Employees;
                File.WriteAllText(this.db, JsonSerializer.Serialize(jsonData));
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public List<Role> GetRoles()
        {
            return JsonSerializer.Deserialize<JsonData>(File.ReadAllText(this.db))?.Roles;
        }

        public bool UpdateRoles(List<Role> Roles)
        {
            try
            {
                JsonData JsonData = JsonSerializer.Deserialize<JsonData>(File.ReadAllText(this.db));
                JsonData.Roles = Roles;
                File.WriteAllText(db, JsonSerializer.Serialize(JsonData));
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}