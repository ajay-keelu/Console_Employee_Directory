namespace EmployeeDirectory.Concerns
{
    public class Employee
    {
        public string? Id { get; set; }

        public string? Name { get; set; }

        // public string? Email { get; set; }

        // public string? MobileNumber { get; set; }

        public string? Location { get; set; } // maintain masterdata

        // public DateTime? DateOfBirth { get; set; }

        public string? JobTitle { get; set; } // maintain masterdata

        public string? Department { get; set; } // maintain masterdata

        public string? Manager { get; set; }

        public string? Project { get; set; }

        public EmployeeStatus Status { get; set; }

        public DateTime? JoiningDate { get; set; }

        // public bool IsActive { get; set; }

        public Employee()
        {
            this.Status = EmployeeStatus.Active;
        }

    }


}