using System.Data;
using EmployeeDirectory.Concerns;
using EmployeeDirectory.Contracts;

namespace EmployeeDirectory.Services
{

    public class EmployeeService : IEmployeeService
    {
        private readonly IDatabaseServices databaseServices;

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

        public List<Employee> GetAssignedEmployees(string Id)
        {
            try
            {
                return (from employee in this.GetAll() where employee.JobTitle == int.Parse(Id) select employee).ToList();
            }
            catch (Exception)
            {
                return new List<Employee>();
            }
        }

        public List<Employee> GetAll()
        {
            DataTable dataTable = this.databaseServices.GetAll<Employee>();
            List<Employee> employees = new List<Employee>();
            foreach (Employee emp in dataTable.Rows)
            {
                employees.Add(emp);
            }
            return employees;
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

        public List<string> GetProperty<T>()
        {
            List<string> propertyList = new List<string>();
            DataTable dataTable = databaseServices.GetMasterData<T>();
            if (typeof(T) == typeof(Location))
            {
                foreach (Location item in dataTable.Rows)
                    propertyList.Add(item.Id.ToString() + " " + item.Name.ToString());
            }
            else if (typeof(T) == typeof(Department))
            {
                foreach (Department item in dataTable.Rows)
                    propertyList.Add(item.Id.ToString() + " " + item.Name.ToString());
            }
            else
            {
                foreach (JobTitle item in dataTable.Rows)
                    propertyList.Add(item.Id.ToString() + " " + item.Name.ToString());
            }
            return propertyList;
        }

        // public bool Update(Employee employee)
        // {
        //     try
        //     {
        //         var employees = this.GetAll();
        //         int index = employees.FindIndex(emp => emp.Id == employee.Id);
        //         employees[index] = employee;

        //         if (!this.databaseServices.Save<Employee>(employees))
        //             throw new Exception();

        //         return true;
        //     }
        //     catch (Exception)
        //     {
        //         return false;
        //     }
        // }

        private string GenerateId()
        {
            DateTime CurrentDate = DateTime.Now;
            return "TZ" + CurrentDate.Year + CurrentDate.Month + CurrentDate.Day + CurrentDate.Hour + CurrentDate.Minute + CurrentDate.Second;
        }
    }

}