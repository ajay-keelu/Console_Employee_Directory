
using System.Data;

namespace EmployeeDirectory.Contracts
{
    public interface IDbConnectionLocal
    {
        public DataTable GetAll<T>();

        public DataTable GetMasterData<T>();

        public bool ExecuteQuery(string query);
    }
}