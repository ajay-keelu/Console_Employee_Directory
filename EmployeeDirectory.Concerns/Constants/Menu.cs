namespace EmployeeDirectory.Concerns
{
    public class Menus
    {
        public static readonly string Management = "1.Employee Management \n2.Role Management\n3.Exit\nEnter your option ";

        public static readonly string Employee = "1.Add Employee\n2.Edit Employee\n3.Delete Employee\n4.Display All\n5.Display One\n6.Go Back to Management Menu\nEnter your option  ";

        public static readonly string Role= "1.Add Role\n2.Edit Role\n3.Delete Role\n4.Display All\n5.Go Back to Management Menu\nEnter your option  ";

        public static readonly string EditRole = "\n1.Role Name\n2.Department\n3.Location\n4.Description\n5.Cancel Edit Role\nEnter your option";

        public static readonly string EditEmployee = "1.Name\n2.Location\n3.Department\n4.Joining date\n5.Role \\ Jobtitle\n6:Date of birth\n7.Email\n8.Manager\n9.Project\n10.Cancel Edit Employee\nEnter your option";

    }
}