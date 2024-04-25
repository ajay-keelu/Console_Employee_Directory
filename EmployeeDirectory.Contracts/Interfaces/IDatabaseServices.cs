
using System.Data;

namespace EmployeeDirectory.Contracts
{
    public interface IDatabaseServices
    {
        public List<T> GetAll<T>(string name) where T : new();


        public bool Create<T>(T item);

        public bool Update<T>(string property, string value, int Id);

        public bool Delete<T>(string id);

        public string GetName(string id, string tableName);
    }
}