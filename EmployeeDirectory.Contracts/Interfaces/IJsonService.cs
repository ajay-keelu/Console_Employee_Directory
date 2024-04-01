
namespace EmployeeDirectory.Contracts
{
    public interface IJsonServices
    {
        public List<T> GetAll<T>();

        public List<string> GetMasterData<T>();
        public bool Save<T>(List<T> list);

    }
}