using EmployeeDirectory.Concerns;
using EmployeeDirectory.Services;
using Microsoft.Extensions.DependencyInjection;

namespace EmployeeDirectory.UI
{

    public class EmployeeDirectory
    {

        public readonly IEmployeeService EmployeeService;
        public readonly IRoleService RoleService;
        public EmployeeDirectory(IEmployeeService employeeService, IRoleService roleService)
        {
            EmployeeService = employeeService;
            RoleService = roleService;
            this.Initialize();
        }
        public void Initialize()
        {

            Console.WriteLine(Menus.ManagementMenu);
            int option;
            Utility.GetOption(out option, 3);

            switch ((MainMenu)option)
            {
                case MainMenu.Employee:
                    this.EmployeeInitalize();
                    break;

                case MainMenu.Role:
                    this.RoleInitialize();
                    break;

                case MainMenu.Exit:
                    Environment.Exit(0);
                    break;
            }
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
                        this.DisplayOne();
                        break;

                    case EmployeeMenu.Back:
                        this.Initialize();
                        break;
                }

                this.EmployeeInitalize();
            }
            catch (Exception)
            {
                Console.WriteLine("Enter options from above");
                this.EmployeeInitalize();
            }
        }

        public void RoleInitialize()
        {
            try
            {
                Console.WriteLine(Menus.RoleMenu);
                int option;
                Utility.GetOption(out option, 5);

                switch ((RoleMenu)option)
                {
                    case RoleMenu.Add:
                        this.AddRole();
                        break;

                    case RoleMenu.Edit:
                        this.EditRole();
                        break;

                    case RoleMenu.Delete:
                        this.DeleteRole();
                        break;

                    case RoleMenu.Display:
                        this.ViewRoles();
                        break;

                    case RoleMenu.Back:
                        this.Initialize();
                        break;
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Enter options from above");
                this.RoleInitialize();
            }
            this.RoleInitialize();
        }

        public void AddEmployee()
        {
            if (RoleService.AreRolesExist())
            {
                Console.WriteLine("------------------------------\nAdd Employee\n------------------------------");

                Employee employee = new Employee()
                {
                    Name = Utility.GetInputString("Fullname", true, RegularExpression.NamePattern),
                    DateOfBirth = Utility.GetInputDate("Date of birth", false),
                    Email = Utility.GetInputEmail(),
                    MobileNumber = Utility.GetMobileNumber(),
                    JoiningDate = Utility.GetInputDate("Joining date", true),
                    Location = Utility.GetInputString("Location", true, null),
                    JobTitle = this.AssignRoleToEmployee(),
                    Department = Utility.GetInputString("Department", true, null),
                    Manager = Utility.GetInputString("Manager ", false, null),
                    Project = Utility.GetInputString("Project ", false, null),
                };

                if (EmployeeService.Save(employee))
                    Console.WriteLine("Employee created successfully. \n");
                else
                    Console.WriteLine("Error in creation of employee.");
            }
            else
            {
                Console.WriteLine("There are no roles available to create an employee. Please create roles to create an employee.");
            }
        }

        public void EditEmployee()
        {
            try
            {
                var Employees = EmployeeService.GetAll();

                if (Employees.Count > 0)
                {
                    this.DisplayEmployees();
                    Console.WriteLine("Enter employee id ");
                    string? id = Console.ReadLine();

                    Employee? employee = EmployeeService.GetById(id ?? "");

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
            catch (Exception)
            {
                Console.WriteLine("Please enter valid employee id.");
                this.EditEmployee();
            }
        }

        public void UpdateEmployee(int option, Employee employee)
        {
            switch ((EditEmployeeMenu)option)
            {
                case EditEmployeeMenu.Name:
                    employee.Name = Utility.GetInputString("Fullname", true, RegularExpression.NamePattern);
                    break;

                case EditEmployeeMenu.Location:
                    employee.Location = Utility.GetInputString("Location", true, null);
                    break;

                case EditEmployeeMenu.Department:
                    employee.Department = Utility.GetInputString("Department", true, null);
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

            EmployeeService.Update(employee);
            Console.WriteLine("\nUpdated successfully.");
            this.EmployeeInitalize();
        }

        public string? AssignRoleToEmployee()
        {
            Console.WriteLine("Select Jobtitle / Role ");

            List<string> list = this.RoleService.GetRoleName();

            foreach (string role in list)
            {
                Console.WriteLine("{0}", role);
            }
            Console.WriteLine("Enter role id");
            string? id = Console.ReadLine();

            if (RoleService.GetById(id ?? "") == null)
            {
                Console.WriteLine("Please enter valid role id.");
                id = this.AssignRoleToEmployee();
            }

            return id;
        }

        public void DisplayOne()
        {
            var Employees = EmployeeService.GetAll();
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

        public void DeleteEmployee()
        {
            var Employees = EmployeeService.GetAll();
            if (Employees.Count > 0)
            {
                this.DisplayEmployees();
                Console.WriteLine("Enter employee id");
                string? id = Console.ReadLine();

                Employee? employee = EmployeeService.GetById(id ?? "");

                if (employee != null)
                {
                    bool status = EmployeeService.DeleteByID(employee.Id);
                    if (status)
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

        public void DisplayEmployees()
        {
            var Employees = EmployeeService.GetAll();
            if (Employees.Count == 0)
                ConsoleUtility.PrintNoData();
            else
                ConsoleUtility.PrintTableHead();

            foreach (Employee employee in Employees)
            {
                ConsoleUtility.PrintEmployeeRow(employee, RoleService.GetById(employee.JobTitle).Name);
                ConsoleUtility.PrintLine();
            }
        }

        public void DisplayEmployees(List<Employee> employees)
        {
            if (employees.Count == 0)
                ConsoleUtility.PrintNoData();
            else
                ConsoleUtility.PrintTableHead();

            foreach (Employee employee in employees)
            {
                Role? role = RoleService.GetById(employee.JobTitle);
                ConsoleUtility.PrintEmployeeRow(employee, role.Name);
                ConsoleUtility.PrintLine();
            }
        }

        public void AddRole()
        {
            Role role = new Role()
            {
                Name = Utility.GetInputString("Role name", true, null),
                Department = Utility.GetInputString("Department ", true, null),
                Location = Utility.GetInputString("Location", true, null),
                Description = Utility.GetInputString("Description ", true, null),
            };

            if (RoleService.Save(role))
                Console.WriteLine("Role created successfully.");
            else
                Console.WriteLine("Please try again!");
        }

        public void EditRole()
        {
            if (RoleService.GetAll().Count == 0)
            {
                ConsoleUtility.PrintNoData();
            }
            else
            {
                try
                {
                    List<string> roles = RoleService.GetRoleName();

                    foreach (var rolename in roles)
                        Console.WriteLine("{0}", rolename);
                    Console.WriteLine("Enter role id ");

                    string? id = Console.ReadLine();
                    Role? role = RoleService.GetById(id ?? "");

                    Console.WriteLine(Menus.EditRoleMenu);
                    int option;
                    Utility.GetOption(out option, 5);

                    if (this.UpdateRole(option, role))
                        Console.WriteLine("Updated successfully.");
                    else
                        Console.WriteLine("Please try again!.");
                }
                catch (Exception)
                {
                    Console.WriteLine("Please enter valid role id.");
                    this.EditRole();
                }
            }
        }

        public bool UpdateRole(int option, Role role)
        {
            try
            {
                switch ((EditRoleMenu)option)
                {
                    case EditRoleMenu.Name:
                        role.Name = Utility.GetInputString("Role name", true, null);
                        break;

                    case EditRoleMenu.Department:
                        role.Department = Utility.GetInputString("Department", true, null);
                        break;

                    case EditRoleMenu.Location:
                        role.Location = Utility.GetInputString("Location", true, null);
                        break;

                    case EditRoleMenu.Description:
                        role.Description = Utility.GetInputString("Description", true, null);
                        break;
                    case EditRoleMenu.Back:
                        this.RoleInitialize();
                        break;
                }
                if (!RoleService.Update(role)) return false;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public void DeleteRole()
        {

            List<string> roles = RoleService.GetRoleName();

            if (roles.Count == 0)
            {
                ConsoleUtility.PrintNoData();
            }
            else
            {
                foreach (string rolename in roles)
                {
                    Console.WriteLine("{0}", rolename);
                }
                Console.WriteLine("Enter role id");
                string? id = Console.ReadLine();

                Role? role = RoleService.GetById(id ?? "");

                if (role != null)
                {
                    if (EmployeeService.GetAssignedEmployee(role.Id).Count > 0)
                    {
                        Console.WriteLine("{0} role contains employees. Please assign employees to another role and then try to delete the role.", role.Name);
                    }
                    else
                    {
                        if (RoleService.DeleteById(id ?? ""))
                            Console.WriteLine("Role deleted successfully");
                        else
                            Console.WriteLine("Please try again!");
                    }
                }
                else
                {
                    Console.WriteLine("Please enter valid role id");
                }
            }
        }

        public void ViewRoles()
        {
            var roles = RoleService.GetAll();
            if (roles.Count == 0)
            {
                ConsoleUtility.PrintNoData();
            }
            else
            {
                Console.WriteLine("+----------------------+");
                Console.WriteLine("|       Roles          |");
                Console.WriteLine("+----------------------+");
                Console.WriteLine("===========================================================================================================================================================================");
            }
            foreach (Role role in roles)
            {
                ConsoleUtility.PrintRoleHeader();
                ConsoleUtility.PrintRoleRow(role);
                this.DisplayEmployees(EmployeeService.GetAssignedEmployee(role.Id));
                Console.WriteLine("===========================================================================================================================================================================");
            }
        }
    }
}