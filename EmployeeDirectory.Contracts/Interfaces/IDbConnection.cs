
using System.Data;

namespace EmployeeDirectory.Contracts
{
    public interface IDbConnectionLocal
    {
        public List<T> GetAll<T>(string name) where T : new();

        public string GetQueryResults(string query);

        public bool ExecuteQuery(string query);
    }
}