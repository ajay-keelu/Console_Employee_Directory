using EmployeeDirectory.Concerns;

namespace EmployeeDirectory.UI
{

    public class ConsoleUtility
    {
        public static void PrintLine()
        {
            Console.WriteLine("+------------------+------------------+------------------+------------------+------------------+--------------------+--------------------+-------------+------------------+");
        }

        public static void PrintNoData()
        {
            Console.WriteLine("+-------------+------------------+------------------+------------------+------------------+-------------+------------------+");
            Console.WriteLine("|                                                    No data found                                                         |");
            Console.WriteLine("+-------------+------------------+------------------+------------------+------------------+-------------+------------------+");
        }

        public static void PrintRoleHeader()
        {
            Console.WriteLine("+------------------+------------------------------+------------------------------+------------------------------+");
            Console.WriteLine("|Role ID           |Role Name                     |Department                    |Location                      |");
            Console.WriteLine("+------------------+------------------------------+------------------------------+------------------------------+");
        }

        public static void ShowEmployees(List<Employee> Employees)
        {
            foreach (var employee in Employees)
            {
                Console.WriteLine("{0}.{1}", employee.Id, employee.Name);
            }
        }

        public static void PrintTableHead()
        {
            Console.WriteLine("+------------------+------------------+------------------+------------------+------------------+--------------------+--------------------+-------------+------------------+");
            Console.WriteLine("|Employee ID       |Full Name         |Location          |Department        |Job Title / Role  |Manager             |Project             |Status       |Joining Date      |");
            Console.WriteLine("+------------------+------------------+------------------+------------------+------------------+--------------------+--------------------+-------------+------------------+");
        }

        public static void PrintEmployeeRow(Employee employee, string roleName)
        {
            string fullname = GetPropertyWithWidth(employee.Name is null or "" ? "No data" : employee.Name, 20);
            string department = GetPropertyWithWidth(employee.Department is null or "" ? "No data" : employee.Department, 20);
            string location = GetPropertyWithWidth(employee.Location is null or "" ? "No data" : employee.Location, 20);
            string role = GetPropertyWithWidth(roleName, 20);
            string status = GetPropertyWithWidth(employee.Status.ToString(), 15);
            string manager = GetPropertyWithWidth(employee.Manager is null or "" ? "No data" : employee.Manager, 22);
            string project = GetPropertyWithWidth(employee.Project is null or "" ? "No data" : employee.Project, 22);
            string joiningDate = GetPropertyWithWidth(employee.JoiningDate?.ToString("dd/MM/yyyy")!, 20);
            string empId = GetPropertyWithWidth(employee.Id!, 20);
            string row = $"""|{empId}|{fullname}|{location}|{department}|{role}|{manager}|{project}|{status}|{joiningDate}|""";
            Console.WriteLine(row);
        }

        public static string GetPropertyWithWidth(string property, int width)
        {
            return property.Length > width - 2 ? property.Substring(0, width - 5) + "..." : property.PadRight(width - 2);
        }

        public static void Print(List<string> list)
        {
            for (int i = 0; i < list.Count; i++)
                Console.WriteLine("{0}. {1}", i + 1, list.ElementAt(i));
        }
        public static void PrintRoleRow(Role role)
        {
            string name = GetPropertyWithWidth(role.Name is null or "" ? "No Data" : role.Name, 32);
            string department = GetPropertyWithWidth(role.Department is null or "" ? "No Data" : role.Department, 32);
            string location = GetPropertyWithWidth(role.Location is null or "" ? "No Data" : role.Location, 32);
            string roleId = GetPropertyWithWidth(role.Id!, 20);
            string row = $$"""|{{roleId}}|{{name}}|{{department}}|{{location}}|""";
            Console.WriteLine(row);
            Console.WriteLine("+------------------+------------------------------+------------------------------+------------------------------+");
            Console.WriteLine("|Description : {0}|", GetPropertyWithWidth(role.Description is null or "" ? "No Data" : role.Description, 99));
            Console.WriteLine("+---------------------------------------------------------------------------------------------------------------+");
            Console.WriteLine("Assigned Employees\n");
        }

    }
}