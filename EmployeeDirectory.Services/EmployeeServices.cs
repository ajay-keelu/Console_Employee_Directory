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

        public List<Employee> GetAssignedEmployees(string Name)
        {
            try
            {
                return (from employee in this.GetAll() where employee.JobTitle == Name select employee).ToList();
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
            foreach (DataRow item in dataTable.Rows)
            {
                Employee emp = new Employee()
                {
                    Id = item["Id"].ToString(),
                    Name = item["Name"].ToString(),
                    JobTitle = item["JobTitle"].ToString(),
                    Location = item["Location"].ToString(),
                    Manager = item["Manager"].ToString(),
                    Project = item["Project"].ToString(),
                    Department = item["Department"].ToString(),
                    Status = (EmployeeStatus)item["Status"],
                    JoiningDate = (DateTime)item["JoiningDate"],
                };
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

            foreach (DataRow item in dataTable.Rows)
                propertyList.Add(item["Id"].ToString() + " " + item["Name"].ToString());

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