using System.Data;
using System.Data.Common;
using EmployeeDirectory.Concerns;
using EmployeeDirectory.Contracts;

namespace EmployeeDirectory.Services
{

    public class EmployeeService : IEmployeeService
    {
        public readonly IDatabaseServices databaseServices;

        public EmployeeService(IDatabaseServices databaseServices)
        {
            this.databaseServices = databaseServices;
        }

        public Employee GetById(string id)
        {
            try
            {
                return (from emp in this.GetAll() where emp.Id == id select emp).Single();
            }
            catch (Exception)
            {
                return null!;
            }
        }

        public List<Employee> GetAssignedEmployees(string id)
        {
            try
            {
                return (from employee in this.GetAll() where employee.Role == id select employee).ToList();
            }
            catch (Exception)
            {
                return new List<Employee>();
            }
        }

        public List<Employee> GetAll()
        {
            return this.databaseServices.GetAll<Employee>("Employee");
        }

        public bool DeleteByID(string id)
        {
            try
            {
                if (!databaseServices.Delete<Employee>(id)) return false;
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public bool Update(string property, string value, int Id)
        {
            try
            {
                if (!databaseServices.Update<Employee>(property, value, Id)) return false;
            }
            catch (System.Exception)
            {
                return false;
            }
            return true;
        }

        public bool Create(Employee employee)
        {
            try
            {
                if (!this.databaseServices.Create<Employee>(employee))
                    throw new Exception();

            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public List<string> GetProperty<T>(string name) where T : new()
        {
            List<T> propertyList = databaseServices.GetAll<T>(name);
            List<string> res = new List<string>();
            if (typeof(T) == typeof(MasterData))
            {
                foreach (T item in propertyList)
                {
                    MasterData md = (MasterData)(object)item;
                    res.Add(md.Id.ToString() + " " + md.Name.ToString());
                }
            }
            return res;
        }


        private string GenerateId()
        {
            DateTime CurrentDate = DateTime.Now;
            return "TZ" + CurrentDate.Year + CurrentDate.Month + CurrentDate.Day + CurrentDate.Hour + CurrentDate.Minute + CurrentDate.Second;
        }
    }

}