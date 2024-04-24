using System.Data;
using System.Data.SqlClient;
using EmployeeDirectory.Concerns;
using EmployeeDirectory.Contracts;
namespace EmployeeDirectory.Database
{
    public class DbConnection : IDbConnectionLocal
    {
        string _connectionString = "data source=sql-dev;initial catalog=ajayEmployeeDirectory;integrated security=SSPI;";
        SqlConnection _sqlConnection;
        SqlCommand sqlCommand;

        public DbConnection()
        {
            _sqlConnection = new SqlConnection(_connectionString);
            sqlCommand = new SqlCommand();
        }

        public DataTable GetAll<T>()
        {
            DataTable dataTable = new DataTable();
            _sqlConnection.Open();
            sqlCommand.Connection = _sqlConnection;
            if (typeof(T) == typeof(Employee))
            {
                sqlCommand.CommandText = "SELECT e.Id, e.Name 'Name', loc.Name 'Location', dept.Name 'Department',jt.Name 'JobTitle', emp.Name 'Manager', proj.Name 'Project', e.Status 'Status', e.JoiningDate 'JoiningDate' FROM  Employee e JOIN Location loc ON e.Location = loc.Id JOIN Department dept ON dept.Id = e.Department JOIN Role r ON e.Role = r.Id JOIN JobTitle jt ON r.Name = jt.Id LEFT JOIN EmployeeMappedProject emproj ON e.Id = emproj.EmpId LEFT JOIN Employee emp ON emp.Id = e.Manager LEFT JOIN Project proj ON proj.Id = emproj.ProjectId WHERE e.Status <> '2';";
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                dataTable.Load(sqlDataReader);
            }
            else
            {
                sqlCommand.CommandText = "SELECT r.Id, jt.Name 'Role Name', loc.Name 'Location', dept.Name 'Department', r.Description 'Description' FROM Role r JOIN Location loc ON r.Location  = loc.Id JOIN Department dept ON r.Department = dept.Id JOIN JobTitle jt ON r.Name = jt.Id";
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                dataTable.Load(sqlDataReader);
            }
            _sqlConnection.Close();
            return dataTable;
        }

        public DataTable GetMasterData<T>()
        {
            DataTable dataTable = new DataTable();
            _sqlConnection.Open();
            sqlCommand.Connection = _sqlConnection;
            if (typeof(T) == typeof(Location))
            {
                sqlCommand.CommandText = "SELECT * FROM Location";
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                dataTable.Load(sqlDataReader);
            }
            else if (typeof(T) == typeof(Department))
            {
                sqlCommand.CommandText = "SELECT * FROM Department";
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                dataTable.Load(sqlDataReader);
            }
            else if (typeof(T) == typeof(JobTitle))
            {
                sqlCommand.CommandText = "SELECT * FROM JobTitle";
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                dataTable.Load(sqlDataReader);
            }
            _sqlConnection.Close();
            return dataTable;
        }

        public bool ExecuteQuery(string query)
        {
            _sqlConnection.Open();
            sqlCommand.Connection = _sqlConnection;
            try
            {
                Console.WriteLine("--- " + query);
                sqlCommand.CommandText = query;
                sqlCommand.ExecuteNonQuery();
                _sqlConnection.Close();
            }
            catch (System.Exception e)
            {
                Console.WriteLine(" --- -- " + e.Message);
                return false;
            }
            _sqlConnection.Close();

            return true;
        }
    }
}