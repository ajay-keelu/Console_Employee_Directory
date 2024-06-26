namespace EmployeeDirectory.Concerns
{
    public enum MainMenu
    {
        Employee = 1,
        Role,
        Exit
    }

    public enum EmployeeStatus
    {
        Active = 1,
        inActive
    }

    public enum EmployeeMenu
    {
        Add = 1,
        Edit,
        Delete,
        DisplayAll,
        DisplayOne,
        Back
    }

    public enum EditEmployeeMenu
    {
        Name = 1,
        Location,
        Department,
        JoiningDate,
        Jobtitle,
        DateOfBirth,
        Email,
        Manager,
        Project,
        Back
    }

    public enum RoleMenu
    {
        Add = 1,
        Edit,
        Delete,
        Display,
        Back
    }

    public enum EditRoleMenu
    {
        Name = 1,
        Department,
        Location,
        Description,
        Back
    }

}