using EmployeeDirectory.Contracts;
using EmployeeDirectory.Concerns;

namespace EmployeeDirectory.UI
{
    public class EmployeeDirectory : IEmployeeDirectory
    {
        public readonly IEmployeeService EmployeeService;

        public readonly IRoleService RoleService;

        public readonly IDatabaseServices DatabaseServices;

        public EmployeeDirectory(IEmployeeService employeeService, IRoleService roleService, IDatabaseServices databaseServices)
        {
            EmployeeService = employeeService;
            RoleService = roleService;
            DatabaseServices = databaseServices;
        }

        public void EmployeeInitalize()
        {
            try
            {
                Console.WriteLine(Menus.Employee);
                int option;
                bool flag = false;
                Utility.GetOption(out option, 6);

                switch ((EmployeeMenu)option)
                {
                    case EmployeeMenu.Add:
                        this.AddEmployee();
                        break;

                    case EmployeeMenu.Edit:
                        this.EditEmployee();
                        break;

                    case EmployeeMenu.Delete:
                        this.DeleteEmployee();
                        break;

                    case EmployeeMenu.DisplayAll:
                        this.DisplayEmployees();
                        break;

                    case EmployeeMenu.DisplayOne:
                        this.DisplayEmployee();
                        this.EmployeeInitalize();
                        break;

                    case EmployeeMenu.Back:
                        flag = true;
                        break;
                }
                if (!flag)
                    this.EmployeeInitalize();

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
                    // Email = Utility.GetInputEmail(),
                    // MobileNumber = Utility.GetMobileNumber(),
                    Role = this.AssignRoleToEmployee(),
                    Location = this.PropertyAssign("Location"),
                    Department = this.PropertyAssign("Department"),
                    // DateOfBirth = Utility.GetInputDate("Date of birth", false),
                    Manager = Utility.GetInputString("Manager ", false, null),
                    // Project = this.PropertyAssign("Project"),
                    JoiningDate = Utility.GetInputDate("Joining date", true),
                    Status = "1"
                };

                if (this.EmployeeService.Create(employee))
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

                    Console.WriteLine(Menus.EditEmployee);
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
            List<string> list;
            int option;
            try
            {
                Console.WriteLine("Select {0} :", prop);
                if (prop.Equals("location"))
                    list = this.EmployeeService.GetProperty<MasterData>("Location");
                else
                    list = this.EmployeeService.GetProperty<MasterData>("Department");

                ConsoleUtility.Print(list);

                Utility.GetOption(out option, list.Count);
                res = list.ElementAt(option - 1);
            }
            catch (System.Exception)
            {
                option = int.Parse(this.PropertyAssign(prop));
            }
            return option.ToString();
        }

        private void UpdateEmployee(int option, Employee employee)
        {
            try
            {
                switch ((EditEmployeeMenu)option)
                {
                    case EditEmployeeMenu.Name:
                        employee.Name = Utility.GetInputString("Fullname", true, RegularExpression.NamePattern);
                        EmployeeService.Update("Name", employee.Name, int.Parse(employee.Id));
                        break;

                    case EditEmployeeMenu.Location:
                        employee.Location = this.PropertyAssign("location");
                        EmployeeService.Update("Name", employee.Location, int.Parse(employee.Id));
                        break;

                    case EditEmployeeMenu.Department:
                        employee.Department = this.PropertyAssign("department");
                        EmployeeService.Update("Name", employee.Department, int.Parse(employee.Id));
                        break;

                    case EditEmployeeMenu.JoiningDate:
                        employee.JoiningDate = Utility.GetInputDate("Joining date", true);
                        EmployeeService.Update("JoiningDate", employee.JoiningDate.ToString(), int.Parse(employee.Id));
                        break;

                    case EditEmployeeMenu.Jobtitle:
                        employee.Role = this.AssignRoleToEmployee();
                        EmployeeService.Update("Role", employee.Role, int.Parse(employee.Id));
                        break;

                    // case EditEmployeeMenu.DateOfBirth:
                    //     employee.DateOfBirth = Utility.GetInputDate("Date of birth", false);
                    //     break;

                    // case EditEmployeeMenu.Email:
                    //     employee.Email = Utility.GetInputEmail();
                    //     break;

                    case EditEmployeeMenu.Manager:
                        employee.Manager = Utility.GetInputString("Manager ", false, null);
                        EmployeeService.Update("Manager", employee.Manager, int.Parse(employee.Id));
                        break;

                    case EditEmployeeMenu.Project:
                        employee.Project = Utility.GetInputString("Project ", false, null);
                        EmployeeService.Update("Project", employee.Project, int.Parse(employee.Id));
                        break;

                    case EditEmployeeMenu.Back:
                        this.EmployeeInitalize();
                        break;
                }
                Console.WriteLine("\nUpdated successfully.");

            }
            catch (System.Exception)
            {
                Console.WriteLine("Oops! Something went wrong");
            }
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
                ConsoleUtility.PrintEmployeeRow(employee, DatabaseServices);
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
                Role? role = this.RoleService.GetById(employee.Role);
                ConsoleUtility.PrintEmployeeRow(employee, DatabaseServices);//, role!.Name!);
                ConsoleUtility.PrintLine();
            }
        }

    }
}