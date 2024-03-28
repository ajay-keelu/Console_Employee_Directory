namespace EmployeeDirectory.Concerns
{


    public class Employee
    {
        public string? Id { get; set; }

        public string? Name { get; set; }

        public string? Location { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public string? Email { get; set; }

        public string? MobileNumber { get; set; }

        public string? JobTitle { get; set; }

        public string? Department { get; set; }

        public string? Manager { get; set; }

        public string? Project { get; set; }

        public Status Status { get; set; }

        public DateTime? JoiningDate { get; set; }

        public bool IsActive { get; set; }

        public Employee()
        {
            this.IsActive = true;
            this.Status = Status.Active;
        }

    }


}