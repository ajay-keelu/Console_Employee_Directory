using EmployeeDirectory.Contracts;
using EmployeeDirectory.Concerns;
using EmployeeDirectory.Services;
using System.Drawing;

namespace EmployeeDirectory.UI
{
    public class EmployeeDirectory : IEmployeeDirectory
    {
        public readonly IEmployeeService EmployeeService;

        public readonly IRoleService RoleService;

        public EmployeeDirectory(IEmployeeService employeeService, IRoleService roleService)
        {
            EmployeeService = employeeService;
            RoleService = roleService;
        }

        public void EmployeeInitalize()
        {
            try
            {
                Console.WriteLine(Menus.EmployeeMenu);
                int option;
                Utility.GetOption(out option, 6);

                switch ((EmployeeMenu)option)
                {
                    case EmployeeMenu.Add:
                        this.AddEmployee();
                        this.EmployeeInitalize();
                        break;

                    case EmployeeMenu.Edit:
                        this.EditEmployee();
                        this.EmployeeInitalize();
                        break;

                    case EmployeeMenu.Delete:
                        this.DeleteEmployee();
                        this.EmployeeInitalize();
                        break;

                    case EmployeeMenu.DisplayAll:
                        this.DisplayEmployees();
                        this.EmployeeInitalize();
                        break;

                    case EmployeeMenu.DisplayOne:
                        this.DisplayEmployee();
                        this.EmployeeInitalize();
                        break;

                    case EmployeeMenu.Back:
                        break;
                }

            }
            catch (Exception)
            {
                this.EmployeeInitalize();
            }
        }

        private void AddEmployee()
        {
            if (RoleService.AreRolesExist())
            {
                Console.WriteLine("------------------------------\nAdd Employee\n------------------------------");

                Employee employee = new Employee()
                {
                    Name = Utility.GetInputString("Fullname", true, RegularExpression.NamePattern),
                    Email = Utility.GetInputEmail(),
                    MobileNumber = Utility.GetMobileNumber(),
                    JobTitle = this.AssignRoleToEmployee(),
                    JoiningDate = Utility.GetInputDate("Joining date", true),
                    Location = this.PropertyAssign("location"),
                    Department = this.PropertyAssign("department"),
                    DateOfBirth = Utility.GetInputDate("Date of birth", false),
                    Manager = Utility.GetInputString("Manager ", false, null),
                    Project = Utility.GetInputString("Project ", false, null),
                };

                if (this.EmployeeService.Save(employee))
                    Console.WriteLine("Employee created successfully. \n");
                else
                    Console.WriteLine("Error in creation of employee.");
            }
            else
            {
                Console.WriteLine("There are no roles available to create an employee. Please create roles to create an employee.");
            }
        }

        private void EditEmployee()
        {
            try
            {
                var Employees = this.EmployeeService.GetAll();

                if (Employees.Count > 0)
                {
                    this.DisplayEmployees();
                    Console.WriteLine("Enter employee id ");
                    string? id = Console.ReadLine();

                    Employee? employee = this.EmployeeService.GetById(id ?? "");

                    if (employee == null)
                        throw new Exception();

                    Console.WriteLine(Menus.EditEmployeeMenu);
                    int option;
                    Utility.GetOption(out option, 10);

                    this.UpdateEmployee(option, employee);
                }
                else
                {
                    this.DisplayEmployees();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine("Please enter valid employee id.");
                this.EditEmployee();
            }
        }

        private string PropertyAssign(string prop)
        {
            string res = "";
            try
            {
                Console.WriteLine("Select {0} :", prop);
                List<string> list = this.EmployeeService.GetProperty<Location>();
                ConsoleUtility.Print(list);

                int option;
                Utility.GetOption(out option, list.Count);
                res = list.ElementAt(option - 1);
            }
            catch (System.Exception)
            {
                res = this.PropertyAssign(prop);
            }
            return res;
        }

        private void UpdateEmployee(int option, Employee employee)
        {
            switch ((EditEmployeeMenu)option)
            {
                case EditEmployeeMenu.Name:
                    employee.Name = Utility.GetInputString("Fullname", true, RegularExpression.NamePattern);
                    break;

                case EditEmployeeMenu.Location:
                    employee.Location = this.PropertyAssign("location");
                    break;

                case EditEmployeeMenu.Department:
                    employee.Department = this.PropertyAssign("department");
                    break;

                case EditEmployeeMenu.JoiningDate:
                    employee.JoiningDate = Utility.GetInputDate("Joining date", true);
                    break;

                case EditEmployeeMenu.Jobtitle:
                    employee.JobTitle = this.AssignRoleToEmployee();
                    break;

                case EditEmployeeMenu.DateOfBirth:
                    employee.DateOfBirth = Utility.GetInputDate("Date of birth", false);
                    break;

                case EditEmployeeMenu.Email:
                    employee.Email = Utility.GetInputEmail();
                    break;

                case EditEmployeeMenu.Manager:
                    employee.Manager = Utility.GetInputString("Manager ", false, null);
                    break;

                case EditEmployeeMenu.Project:
                    employee.Project = Utility.GetInputString("Project ", false, null);
                    break;

                case EditEmployeeMenu.Back:
                    this.EmployeeInitalize();
                    break;
            }

            EmployeeService.Save(employee);
            Console.WriteLine("\nUpdated successfully.");

            this.EmployeeInitalize();
        }

        private string? AssignRoleToEmployee()
        {
            Console.WriteLine("Select Jobtitle / Role ");

            List<string> list = this.RoleService.GetRoleName();

            foreach (string role in list)
            {
                Console.WriteLine("{0}", role);
            }

            Console.WriteLine("Enter role id");
            string? id = Console.ReadLine();

            if (this.RoleService.GetById(id ?? "") == null)
            {
                Console.WriteLine("Please enter valid role id.");
                id = this.AssignRoleToEmployee();
            }

            return id;
        }

        private void DisplayEmployee()
        {
            var Employees = this.EmployeeService.GetAll();
            if (Employees.Count > 0)
            {
                try
                {
                    ConsoleUtility.ShowEmployees(Employees);
                    Console.WriteLine("Emter employee id.");
                    string? id = Console.ReadLine();

                    Employee? employee = EmployeeService.GetById(id ?? "");

                    if (employee == null)
                        throw new Exception();

                    this.DisplayEmployees(new List<Employee> { employee });
                }
                catch (Exception)
                {
                    Console.WriteLine("Please enter valid employee id.");
                }
            }
            else
            {
                ConsoleUtility.PrintNoData();
            }
        }

        private void DeleteEmployee()
        {
            var Employees = EmployeeService.GetAll();

            if (Employees.Count > 0)
            {
                this.DisplayEmployees();
                Console.WriteLine("Enter employee id");
                string? id = Console.ReadLine();

                Employee employee = EmployeeService.GetById(id ?? "");

                if (employee != null)
                {
                    if (EmployeeService.DeleteByID(employee.Id!))
                        Console.WriteLine("Employee deleted successfully.");
                    else
                        Console.WriteLine("Please try again!.");
                }
                else
                {
                    Console.WriteLine("Please enter valid employee id.");
                    this.DeleteEmployee();
                }
            }
            else
            {
                this.DisplayEmployees();
            }
        }

        private void DisplayEmployees()
        {
            var Employees = this.EmployeeService.GetAll();
            if (Employees.Count > 0)
                ConsoleUtility.PrintTableHead();
            else
                ConsoleUtility.PrintNoData();

            foreach (Employee employee in Employees)
            {
                ConsoleUtility.PrintEmployeeRow(employee, this.RoleService.GetById(employee.JobTitle!)!.Name!);
                ConsoleUtility.PrintLine();
            }
        }

        private void DisplayEmployees(List<Employee> employees)
        {
            if (employees.Count == 0)
                ConsoleUtility.PrintNoData();
            else
                ConsoleUtility.PrintTableHead();

            foreach (Employee employee in employees)
            {
                Role? role = this.RoleService.GetById(employee.JobTitle!);
                ConsoleUtility.PrintEmployeeRow(employee, role!.Name!);
                ConsoleUtility.PrintLine();
            }
        }

    }
}