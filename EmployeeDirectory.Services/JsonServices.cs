using System.Text.Json;
using EmployeeDirectory.Concerns;

namespace EmployeeDirectory.Services
{

    public class JsonServices : IJsonServices
    {
        public readonly string db = @"../EmployeeDirectory.Database/db.json";

        public List<Employee> GetEmployees()
        {

            return JsonSerializer.Deserialize<JsonData>(File.ReadAllText(db))?.Employees;
        }

        public bool UpdateEmployees(List<Employee> Employees)
        {
            try
            {
                JsonData jsonData = JsonSerializer.Deserialize<JsonData>(File.ReadAllText(db));
                jsonData.Employees = Employees;
                string jsonString = JsonSerializer.Serialize(jsonData);
                File.WriteAllText(db, jsonString);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public List<Role> GetRoles()
        {
            return JsonSerializer.Deserialize<JsonData>(File.ReadAllText(db))?.Roles;
        }

        public bool UpdateRoles(List<Role> Roles)
        {
            try
            {
                JsonData JsonData = JsonSerializer.Deserialize<JsonData>(File.ReadAllText(db));
                JsonData.Roles = Roles;
                string jsonString = JsonSerializer.Serialize(JsonData);
                File.WriteAllText(db, jsonString);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}