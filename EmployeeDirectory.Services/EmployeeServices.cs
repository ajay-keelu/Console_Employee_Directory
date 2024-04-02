using System.Text.Json;
using EmployeeDirectory.Concerns;
using EmployeeDirectory.Contracts;

namespace EmployeeDirectory.Services
{

    public class EmployeeService : IEmployeeService
    {
        private readonly IJsonServices _jsonServices;

        public EmployeeService(IJsonServices jsonServices)
        {
            this._jsonServices = jsonServices;
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
                return (from employee in this.GetAll() where employee.JobTitle == Id select employee).ToList();
            }
            catch (Exception)
            {
                return new List<Employee>();
            }
        }

        public List<Employee> GetAll()
        {
            return (from employee in this._jsonServices.GetAll<Employee>() where employee.IsActive select employee).ToList<Employee>();
        }

        public bool DeleteByID(string id)
        {
            try
            {
                var employees = this.GetAll();
                employees = employees.FindAll(emp => emp.Id != id);
                if (!this._jsonServices.Save<Employee>(employees))
                    throw new Exception();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Save(Employee employee)
        {
            if (string.IsNullOrEmpty(employee.Id))
                return this.Create(employee);
            else
                return this.Update(employee);
        }

        private bool Create(Employee employee)
        {
            try
            {
                employee.Id = this.GenerateId();

                var employees = this.GetAll();
                employees.Add(employee);

                if (!this._jsonServices.Save<Employee>(employees))
                    throw new Exception();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<string> GetProperty<T>()
        {
            return _jsonServices.GetMasterData<T>();
        }

        public bool Update(Employee employee)
        {
            try
            {
                var employees = this.GetAll();
                int index = employees.FindIndex(emp => emp.Id == employee.Id);
                employees[index] = employee;

                if (!this._jsonServices.Save<Employee>(employees))
                    throw new Exception();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private string GenerateId()
        {
            DateTime CurrentDate = DateTime.Now;
            return "TZ" + CurrentDate.Year + CurrentDate.Month + CurrentDate.Day + CurrentDate.Hour + CurrentDate.Minute + CurrentDate.Second;
        }
    }

}