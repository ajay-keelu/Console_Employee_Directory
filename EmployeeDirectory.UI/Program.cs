
using EmployeeDirectory.Concerns;
using EmployeeDirectory.Services;
using Microsoft.Extensions.DependencyInjection;

public class Program
{

    static void Main()
    {
        var serviceProvider = new ServiceCollection()
        .AddSingleton<IJsonServices, JsonServices>()
        .AddSingleton<IRoleService, RoleService>()
        .AddSingleton<IEmployeeService, EmployeeService>()
        .BuildServiceProvider();
        IEmployeeService employeeService = serviceProvider.GetRequiredService<IEmployeeService>();
        IRoleService roleService = serviceProvider.GetRequiredService<IRoleService>();
        new EmployeeDirectory.UI.EmployeeDirectory(employeeService, roleService).Initialize();
    }

}