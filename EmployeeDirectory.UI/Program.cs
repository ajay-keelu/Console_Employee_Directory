
using EmployeeDirectory.Concerns;
using EmployeeDirectory.Services;
using Microsoft.Extensions.DependencyInjection;

public class Program
{

    static void Main()
    {
        Initialize();
    }
    static void Initialize()
    {
        ServiceProvider serviceProvider = Configure();
        IEmployeeService employeeService = serviceProvider.GetRequiredService<IEmployeeService>();
        IRoleService roleService = serviceProvider.GetRequiredService<IRoleService>();
        new EmployeeDirectory.UI.EmployeeDirectory(employeeService, roleService).Initialize();
    }
    static ServiceProvider Configure()
    {
        return new ServiceCollection()
                .AddSingleton<IJsonServices, JsonServices>()
                .AddSingleton<IRoleService, RoleService>()
                .AddSingleton<IEmployeeService, EmployeeService>()
                .BuildServiceProvider();
    }

}