using EmployeeDirectory.Concerns;
using EmployeeDirectory.Contracts;

namespace EmployeeDirectory.UI
{
    public class RoleDirectory : IRoleDirectory
    {
        public readonly IEmployeeService EmployeeService;

        public readonly IRoleService RoleService;

        public RoleDirectory(IEmployeeService employeeService, IRoleService roleService)
        {
            EmployeeService = employeeService;
            RoleService = roleService;
        }

        public void RoleInitialize()
        {
            try
            {
                Console.WriteLine(Menus.RoleMenu);
                int option;
                Utility.GetOption(out option, 5);
                bool flag = false;
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
                        flag = true;
                        break;
                }

                if (!flag)
                    this.RoleInitialize();

            }
            catch (Exception)
            {
                Console.WriteLine("Enter options from above");
                this.RoleInitialize();
            }

        }

        private void AddRole()
        {
            Role role = new Role()
            {
                Name = this.GetRoleProperty<JobTitle>(),
                Department = this.GetRoleProperty<Department>(),
                Location = this.GetRoleProperty<Location>(),
                Description = Utility.GetInputString("Description ", true, null),
            };

            if (this.RoleService.Create(role))
                Console.WriteLine("Role created successfully.");
            else
                Console.WriteLine("Please try again!");
        }

        private string GetRoleProperty<T>()
        {
            string res = "";
            int option;
            try
            {
                Console.WriteLine("Select {0} :", typeof(T).ToString().ToLower());
                List<string> list = this.RoleService.GetProperty<T>();
                ConsoleUtility.Print(list);
                Utility.GetOption(out option, list.Count);
                res = list.ElementAt(option - 1);
            }
            catch (System.Exception e)
            {
                Console.WriteLine(e.Message);
                option = int.Parse(this.GetRoleProperty<T>());
            }
            return option.ToString();
        }

        private void EditRole()
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

                    if (role == null) throw new Exception();

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

        private bool UpdateRole(int option, Role role)
        {
            try
            {
                switch ((EditRoleMenu)option)
                {
                    case EditRoleMenu.Name:
                        role.Name = this.GetRoleProperty<JobTitle>();
                        this.RoleService.Update("Name", role.Name.ToString(), int.Parse(role.Id));
                        break;

                    case EditRoleMenu.Department:
                        role.Department = this.GetRoleProperty<Department>();
                        this.RoleService.Update("Department", role.Department.ToString(), int.Parse(role.Id));
                        break;

                    case EditRoleMenu.Location:
                        role.Location = this.GetRoleProperty<Location>();
                        this.RoleService.Update("Department", role.Location.ToString(), int.Parse(role.Id));
                        break;

                    case EditRoleMenu.Description:
                        role.Description = Utility.GetInputString("Description", true, null);
                        this.RoleService.Update("Department", role.Description, int.Parse(role.Id));
                        break;
                    case EditRoleMenu.Back:
                        this.RoleInitialize();
                        break;
                }
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        private void DeleteRole()
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
                    Console.WriteLine(rolename);
                }
                Console.WriteLine("Enter role id");
                string? id = Console.ReadLine();

                Role? role = this.RoleService.GetById(id ?? "");

                if (role != null)
                {
                    if (this.EmployeeService.GetAssignedEmployees(role.Id!).Count > 0)
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

        private void DisplayRoles()
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

        private void DisplayEmployees(List<Employee> employees)
        {
            if (employees.Count == 0)
                ConsoleUtility.PrintNoData();
            else
                ConsoleUtility.PrintTableHead();

            foreach (Employee employee in employees)
            {
                Role? role = this.RoleService.GetById(employee.JobTitle.ToString()!);
                ConsoleUtility.PrintEmployeeRow(employee, role?.Name!);
                ConsoleUtility.PrintLine();
            }
        }

    }
}