using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using EmployeeDirectory.Contracts;

namespace EmployeeDirectory.Database
{
    public class DbConnection : IDbConnectionLocal
    {
        string _connectionString = "data source=sql-dev;initial catalog=ajayEmployeeDirectory;integrated security=SSPI;";
        private SqlConnection _sqlConnection;

        public DbConnection(SqlConnection sqlConnection)
        {
            _sqlConnection = sqlConnection;
            _sqlConnection.ConnectionString = _connectionString;
        }

        public List<T> GetAll<T>(string tableName) where T : new()
        {
            List<T> list = new List<T>();
            _sqlConnection.Open();
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.CommandText = $"SELECT * FROM {tableName};";
            sqlCommand.Connection = _sqlConnection;
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            while (sqlDataReader.Read())
            {
                T obj = new T();
                Type type = obj.GetType();

                for (int i = 0; i < sqlDataReader.FieldCount; i++)
                {
                    string fieldName = sqlDataReader.GetName(i);
                    PropertyInfo prop = type.GetProperty(fieldName);
                    if (prop != null && sqlDataReader[fieldName] != DBNull.Value)
                    {
                        object value = sqlDataReader[fieldName];
                        prop.SetValue(obj, Convert.ChangeType(value, prop.PropertyType));
                    }
                }
                list.Add(obj);
            }
            _sqlConnection.Close();
            return list;
        }

        public string GetQueryResults(string query)
        {
            string res;
            try
            {
                _sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = _sqlConnection;
                sqlCommand.CommandText = query;
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                res = sqlDataReader.GetString(0);
            }
            catch (Exception e)
            {
                _sqlConnection.Close();
                Console.WriteLine("--- " + e.Message);
                res = GetQueryResults(query);
            }
            finally
            {
                _sqlConnection.Close();
            }

            return res;
        }

        public bool ExecuteQuery(string query)
        {
            try
            {
                _sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = _sqlConnection;
                sqlCommand.CommandText = query;
                int effectedRows = sqlCommand.ExecuteNonQuery();
            }
            catch (System.Exception)
            {
                return false;
            }
            finally
            {
                _sqlConnection.Close();
            }

            return true;
        }
    }
}