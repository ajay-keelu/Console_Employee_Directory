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
                    list = JsonSerializer.Deserialize<List<Employee>>(File.ReadAllText(this.db + @"Employees.json"))!.Cast<T>().ToList();
                    foreach (var item in list)
                    {
                        Console.WriteLine(item!.ToString());
                    }
                }
                else
                {
                    list = JsonSerializer.Deserialize<List<Role>>(File.ReadAllText(this.db + @"Roles.json"))!.Cast<T>().ToList();
                }
            }
            catch (System.Exception e)
            {
                Console.WriteLine(e.Message);
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
                    list = JsonSerializer.Deserialize<Location>(File.ReadAllText(this.db + "Locations.json"))!.Locations!;
                }
                else if (typeof(T) == typeof(Department))
                {
                    list = JsonSerializer.Deserialize<Department>(File.ReadAllText(this.db + "Locations.json"))!.Departments!;
                }
                else
                {
                    list = JsonSerializer.Deserialize<JobTitle>(File.ReadAllText(this.db + "Locations.json"))!.JobTitles!;
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
            List<T> jsonData;
            try
            {
                if (typeof(T) == typeof(Employee))
                {
                    jsonData = JsonSerializer.Deserialize<List<Employee>>(File.ReadAllText(this.db + "Employees.json"))!.Cast<T>().ToList();

                }
                else
                {
                    jsonData = JsonSerializer.Deserialize<List<Role>>(File.ReadAllText(this.db + "Employees.json"))!.Cast<T>().ToList();
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