namespace EmployeeDirectory.Concerns
{

    public class Role
    {
        public string? Id { get; set; }

        public string? Name { get; set; }

        public string? Department { get; set; }

        public string? Description { get; set; }

        public string? Location { get; set; }

        public List<string> AssignedEmployees { get; set; }

        public bool IsActive { get; set; }

        public Role()
        {
            this.AssignedEmployees = new List<string>();
            this.IsActive = true;
        }
    }
}