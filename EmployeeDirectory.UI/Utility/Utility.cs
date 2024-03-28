using System.Text.RegularExpressions;

namespace EmployeeDirectory.UI
{

    public class Utility
    {
        public static void GetOption(out int option, int options)
        {
            try
            {
                option = int.Parse(Console.ReadLine().Trim());
                if (option <= 0 || option > options) throw new Exception();
            }
            catch (System.Exception)
            {
                Console.WriteLine("\nEnter options from above .");
                GetOption(out option, options);
            }
        }

        public static bool IsValidProperty(string s, string pattern)
        {
            return Regex.IsMatch(s, pattern);
        }

        public static string GetInputString(string propertyName, bool isRequired, string? pattern)
        {
            string? input;
            try
            {
                Console.WriteLine("Enter {0}{1}", propertyName, isRequired ? "*" : "");
                input = Console.ReadLine()?.Trim();

                if (isRequired && string.IsNullOrEmpty(input))
                    throw new Exception();

                if (pattern != null && !IsValidProperty(input ??= "", pattern))
                    throw new Exception();
            }
            catch (Exception)
            {
                Console.WriteLine("Please enter valid {0}  ... ", propertyName);
                input = GetInputString(propertyName, isRequired, pattern);
            }
            return input;
        }

        public static DateTime GetInputDate(string propertyName, bool isRequired)
        {
            string? date;
            try
            {
                Console.WriteLine("Enter {0}{1}", propertyName, isRequired ? "*" : "");
                date = Console.ReadLine()?.Trim();

                if (!string.IsNullOrEmpty(date) && !IsValidProperty(date, RegularExpression.DatePattern))
                    throw new Exception();

                if (isRequired && !IsValidProperty(date ??= "", RegularExpression.DatePattern))
                    throw new Exception();

                return DateTime.Parse(date is null or "" ? DateTime.MinValue.ToString("dd/MM/yyyy") : date);
            }
            catch (Exception)
            {
                Console.WriteLine("Please enter valid {0}  ... ", propertyName);
                return GetInputDate(propertyName, isRequired);
            }
        }

        public static string GetInputEmail()
        {
            string? email;
            try
            {
                Console.WriteLine("Enter email*");
                email = Console.ReadLine()?.Trim();

                if (!IsValidProperty(email ??= "", RegularExpression.EmailPattern))
                    throw new Exception();
            }
            catch (Exception)
            {
                Console.WriteLine("Please enter valid email  ... ");
                email = GetInputEmail();
            }
            return email;
        }

        public static string GetMobileNumber()
        {
            string? mobile;
            try
            {
                Console.WriteLine("Enter mobile number ");
                mobile = Console.ReadLine()?.Trim();
                if (!string.IsNullOrEmpty(mobile ??= "") && !IsValidProperty(mobile, RegularExpression.MobilePattern))
                    throw new Exception();
            }
            catch (Exception)
            {
                Console.WriteLine("Please enter valid mobile number ");
                mobile = GetMobileNumber();
            }
            return mobile;
        }

    }
}