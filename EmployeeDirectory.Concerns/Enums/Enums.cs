namespace EmployeeDirectory.Concerns
{

    public enum MainMenu
    {
        Employee = 1,
        Role = 2,
        Exit = 3
    }

    public enum Status
    {
        Active = 1,
        inActive = 2
    }

    public enum EmployeeMenu
    {
        Add = 1,
        Edit = 2,
        Delete = 3,
        DisplayAll = 4,
        DisplayOne = 5,
        Back = 6
    }

    public enum EditEmployeeMenu
    {
        Name = 1,
        Location = 2,
        Department = 3,
        JoiningDate = 4,
        Jobtitle = 5,
        DateOfBirth = 6,
        Email = 7,
        Manager = 8,
        Project = 9,
        Back = 10
    }

    public enum RoleMenu
    {
        Add = 1,
        Edit = 2,
        Delete = 3,
        Display = 4,
        Back = 5
    }

    public enum EditRoleMenu
    {
        Name = 1,
        Department = 2,
        Location = 3,
        Description = 4,
        Back = 5
    }

}