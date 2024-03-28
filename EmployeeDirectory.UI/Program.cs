
using EmployeeDirectory.Concerns;
using EmployeeDirectory.Services;
using Microsoft.Extensions.DependencyInjection;

public class Program
{

    static void Main()
    {
        var serviceProvider = new ServiceCollection().AddSingleton<IRoleService, RoleService>().AddSingleton<IEmployeeService, EmployeeService>().BuildServiceProvider();
        IRoleService RoleService = serviceProvider.GetService<IRoleService>()!;
        IEmployeeService EmployeeService = serviceProvider.GetService<IEmployeeService>()!;
        EmployeeDirectory.UI.EmployeeDirectory employeeDirectory = new EmployeeDirectory.UI.EmployeeDirectory(EmployeeService, RoleService);
    }

}