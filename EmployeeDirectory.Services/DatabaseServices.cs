using System.Data;
using EmployeeDirectory.Concerns;
using EmployeeDirectory.Contracts;

namespace EmployeeDirectory.Services
{

    public class DatabaseServices : IDatabaseServices
    {
        private readonly IDbConnectionLocal dbConnectionLocal;
        public DatabaseServices(IDbConnectionLocal dbConnectionLocal)
        {
            this.dbConnectionLocal = dbConnectionLocal;
        }

        public List<T> GetAll<T>(string tableName) where T : new()
        {
            return dbConnectionLocal.GetAll<T>(tableName);
        }

        public bool Create<T>(T record)
        {
            try
            {
                if (record == null) return false;

                string query;
                if (record is Employee)
                {
                    Employee employee = (Employee)(object)record;
                    query = $"INSERT INTO Employee(Name,Location,Department,Role,Manager,Status,JoiningDate) VALUES('{employee.Name}',{employee.Location},{employee.Department},{employee.Role},{(string.IsNullOrEmpty(employee.Manager) ? "NULL" : employee.Manager)},{employee.Status},'{employee.JoiningDate}')";
                }
                else
                {
                    Role role = (Role)(object)record;
                    query = $"INSERT INTO Role VALUES({role.Name},{role.Location},{role.Department},'{role.Description}')";
                }
                if (!dbConnectionLocal.ExecuteQuery(query)) return false;
            }
            catch (System.Exception)
            {
                return false;
            }
            return true;
        }

        public string GetName(string id, string tableName)
        {
            return dbConnectionLocal.GetQueryResults($"SELECT Name FROM {tableName} WHERE Id ={id}");
        }

        public bool Update<T>(string property, string value, int Id)
        {
            try
            {
                string query;
                if (typeof(T) == typeof(Employee))
                {
                    query = $"UPDATE Employee SET {property} = '{value}' WHERE Id = {Id}";
                }
                else
                {
                    query = $"UPDATE Role SET {property} = '{value}' WHERE Id = {Id}";
                }
                if (!dbConnectionLocal.ExecuteQuery(query)) return false;
            }
            catch (System.Exception)
            {
                return false;
            }
            return true;
        }

        public bool Delete<T>(string Id)
        {
            try
            {
                if (typeof(T) == typeof(Employee))
                {
                    if (!dbConnectionLocal.ExecuteQuery($"UPDATE Employee SET Status = 2 WHERE Id = {Id}")) return false;
                }
                else
                {
                    if (!dbConnectionLocal.ExecuteQuery($"UPDATE Role SET Status = 2 WHERE Id = {Id}")) return false;
                }
            }
            catch (System.Exception)
            {
                return false;
            }
            return true;
        }
    }
}