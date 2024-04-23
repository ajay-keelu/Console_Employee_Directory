using System.Data;
using System.Data.SqlClient;
using EmployeeDirectory.Concerns;
using EmployeeDirectory.Contracts;
namespace EmployeeDirectory.Database
{
    public class DbConnection : IDbConnectionLocal
    {
        string _connectionString = "DATA SOURCE=sql-dev;INITIAL CATALOG=ajayEmployeeDirectory;INTEGRATED SECURITY=SSPI;";
        SqlConnection _sqlConnection;
        SqlCommand sqlCommand;

        public DbConnection()
        {
            using (_sqlConnection = new SqlConnection(_connectionString))
            {
                sqlCommand = new SqlCommand();
                sqlCommand.Connection = _sqlConnection;
            }
        }

        public DataTable GetAll<T>()
        {
            DataTable dataTable = new DataTable();
            if (typeof(T) == typeof(Employee))
            {
                _sqlConnection.Open();
                sqlCommand.CommandText = "SELECT e.Id, e.Name, loc.Name 'Location', dept.Name 'Department', emp.Name 'Manager', proj.Name 'Project', s.Name 'Status', e.JoiningDate 'Joining Date' FROM  Employee e JOIN Location loc ON e.Location = loc.Id JOIN Department dept ON dept.Id = e.Department JOIN Status s ON e.Status = s.Id LEFT JOIN EmployeeMappedProject emproj ON e.Id = emproj.EmpId LEFT JOIN Employee emp ON emp.Id = e.Manager LEFT JOIN Project proj ON proj.Id = emproj.ProjectId WHERE e.Status <> '2';";
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                dataTable.Load(sqlDataReader);
                _sqlConnection.Close();
            }
            else
            {
                _sqlConnection.Open();
                sqlCommand.CommandText = "SELECT r.Id, 'Role Name', loc.Name 'Location', dept.Name 'Department', r.Description 'Description' FROM Role r JOIN Location loc ON r.Location  = loc.Id JOIN Department dept ON r.Department = dept.Id";
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                dataTable.Load(sqlDataReader);
                _sqlConnection.Close();
            }
            return dataTable;
        }

        public DataTable GetMasterData<T>()
        {
            DataTable dataTable = new DataTable();
            if (typeof(T) == typeof(Location))
            {
                _sqlConnection.Open();
                sqlCommand.CommandText = "SELECT * FROM Location";
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                dataTable.Load(sqlDataReader);
                _sqlConnection.Close();
            }
            else if (typeof(T) == typeof(Department))
            {
                _sqlConnection.Open();
                sqlCommand.CommandText = "SELECT * FROM Department";
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                dataTable.Load(sqlDataReader);
                _sqlConnection.Close();
            }
            else if (typeof(T) == typeof(JobTitle))
            {
                _sqlConnection.Open();
                sqlCommand.CommandText = "SELECT * FROM JobTitle";
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                dataTable.Load(sqlDataReader);
                _sqlConnection.Close();
            }
            return dataTable;
        }

        public bool ExecuteQuery(string query)
        {
            try
            {
                _sqlConnection.Open();
                sqlCommand.CommandText = query;
                _sqlConnection.Close();
            }
            catch (System.Exception)
            {
                return false;
            }
            return true;
        }
    }
}