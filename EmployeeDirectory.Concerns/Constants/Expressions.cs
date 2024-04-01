namespace EmployeeDirectory.Concerns
{

    public class RegularExpression
    {
        public static readonly string EmailPattern = @"[a-zA-Z0-9.-_]+@[a-zA-Z.-]+\.[a-zA-z]{2,4}$";

        public static readonly string MobilePattern = @"^[1-9]{1}[0-9]{9}$";

        public static readonly string DatePattern = @"[0-9]{1,2}\/[0-9]{1,2}\/([0-9]{4}|[0-9]{2})$";

        public static readonly string NamePattern = @"^[a-zA-Z]+\ [a-zA-Z]+$";

    }
}