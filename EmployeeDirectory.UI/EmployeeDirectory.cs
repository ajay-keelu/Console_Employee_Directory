using EmployeeDirectory.Contracts;
using EmployeeDirectory.Concerns;
using EmployeeDirectory.Services;

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
                        this.DisplayEmployee();
                        break;

                    case EmployeeMenu.Back:
                        this.Initialize();
                        break;
                }

            }
            catch (Exception)
            {
                Console.WriteLine("Enter options from above");
            }

            this.EmployeeInitalize();
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
                        this.DisplayRoles();
                        break;

                    case RoleMenu.Back:
                        this.Initialize();
                        break;
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Enter options from above");
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

        public void EditEmployee()
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
            catch (Exception)
            {
                Console.WriteLine("Please enter valid employee id.");
                this.EditEmployee();
            }
        }

        public string PropertyAssign(string prop)
        {
            string res = "";
            try
            {
                Console.WriteLine("Select {0} :", prop);
                List<string> list = this.EmployeeService.GetProperty(prop);
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

        public void UpdateEmployee(int option, Employee employee)
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

            if (this.RoleService.GetById(id ?? "") == null)
            {
                Console.WriteLine("Please enter valid role id.");
                id = this.AssignRoleToEmployee();
            }

            return id;
        }

        public void DisplayEmployee()
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

        public void DeleteEmployee()
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
                    if (EmployeeService.DeleteByID(employee.Id))
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
            var Employees = this.EmployeeService.GetAll();
            if (Employees.Count == 0)
                ConsoleUtility.PrintNoData();
            else
                ConsoleUtility.PrintTableHead();

            foreach (Employee employee in Employees)
            {
                ConsoleUtility.PrintEmployeeRow(employee, this.RoleService.GetById(employee.JobTitle).Name);
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
                Role? role = this.RoleService.GetById(employee.JobTitle);
                ConsoleUtility.PrintEmployeeRow(employee, role.Name);
                ConsoleUtility.PrintLine();
            }
        }

        public void AddRole()
        {
            Role role = new Role()
            {
                Name = this.GetRole(),
                Department = Utility.GetInputString("Department ", true, null),
                Location = Utility.GetInputString("Location", true, null),
                Description = Utility.GetInputString("Description ", true, null),
            };

            if (this.RoleService.Save(role))
                Console.WriteLine("Role created successfully.");
            else
                Console.WriteLine("Please try again!");
        }

        public string GetRole()
        {
            string res = "";
            try
            {
                int option;
                var roles = RoleService.GetProperty();
                Console.WriteLine("Select Role :");
                ConsoleUtility.Print(roles);
                Utility.GetOption(out option, roles.Count);
                res = roles.ElementAt(option - 1);
            }
            catch (System.Exception)
            {
                res = this.GetRole();
            }
            return res;
        }

        public void EditRole()
        {
            if (this.RoleService.GetAll().Count == 0)
            {
                ConsoleUtility.PrintNoData();
            }
            else
            {
                try
                {
                    List<string> roles = this.RoleService.GetRoleName();

                    foreach (var rolename in roles)
                        Console.WriteLine("{0}", rolename);
                    Console.WriteLine("Enter role id ");

                    string? id = Console.ReadLine();
                    Role? role = this.RoleService.GetById(id ?? "");

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
                        role.Name = this.GetRole();
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
                if (!RoleService.Save(role)) return false;
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public void DeleteRole()
        {

            List<string> roles = this.RoleService.GetRoleName();

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

                Role? role = this.RoleService.GetById(id ?? "");

                if (role != null)
                {
                    if (this.EmployeeService.GetAssignedEmployees(role.Id).Count > 0)
                    {
                        Console.WriteLine("{0} role contains employees. Please assign employees to another role and then try to delete the role.", role.Name);
                    }
                    else
                    {
                        if (this.RoleService.DeleteById(id ?? ""))
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

        public void DisplayRoles()
        {
            var roles = this.RoleService.GetAll();
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
                this.DisplayEmployees(this.EmployeeService.GetAssignedEmployees(role.Id));
                Console.WriteLine("===========================================================================================================================================================================");
            }
        }
    }
}