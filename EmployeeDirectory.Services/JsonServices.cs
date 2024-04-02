using System.Text.Json;
using EmployeeDirectory.Concerns;
using EmployeeDirectory.Contracts;

namespace EmployeeDirectory.Services
{

    public class JsonServices : IJsonServices
    {
        public readonly string db = @"../EmployeeDirectory.Database/json/";

        public List<T> GetAll<T>()
        {
            List<T> list = new List<T>();
            try
            {

                if (typeof(T) == typeof(Employee))
                {
                    list = JsonSerializer.Deserialize<List<Employee>>(File.ReadAllText(this.db + "Employees.json"))!.Cast<T>().ToList();
                }
                else
                {
                    list = JsonSerializer.Deserialize<List<Role>>(File.ReadAllText(this.db + "Roles.json"))!.Cast<T>().ToList();
                }
            }
            catch (System.Exception)
            {
                list = this.GetAll<T>();
            }
            return list;
        }

        public List<string> GetMasterData<T>()
        {
            List<string> list = new List<string>();
            try
            {
                if (typeof(T) == typeof(Location))
                {
                    list = JsonSerializer.Deserialize<List<string>>(File.ReadAllText(this.db + "Locations.json"))!;
                }
                else if (typeof(T) == typeof(Department))
                {
                    list = JsonSerializer.Deserialize<List<string>>(File.ReadAllText(this.db + "Departments.json"))!;
                }
                else
                {
                    list = JsonSerializer.Deserialize<List<string>>(File.ReadAllText(this.db + "JobTitles.json"))!;
                }
            }
            catch (System.Exception)
            {
                list = this.GetMasterData<T>();
            }
            return list;
        }

        public bool Save<T>(List<T> list)
        {
            try
            {
                string jsonData;
                if (typeof(T) == typeof(Employee))
                {
                    jsonData = JsonSerializer.Serialize(list);
                    File.WriteAllText(db + @"Employees.json", jsonData);
                }
                else
                {
                    jsonData = JsonSerializer.Serialize(list);
                    File.WriteAllText(db + @"Roles.json", jsonData);
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}