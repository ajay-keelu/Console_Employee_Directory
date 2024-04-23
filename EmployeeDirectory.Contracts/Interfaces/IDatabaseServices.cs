
using System.Data;

namespace EmployeeDirectory.Contracts
{
    public interface IDatabaseServices
    {
        public DataTable GetAll<T>();

        public DataTable GetMasterData<T>();

        public bool Update<T>(string property, string value, int Id);

        public bool Create<T>(T record);

        public bool Delete<T>(string id);

    }
}