using Microsoft.Extensions.DependencyInjection;
using EmployeeDirectory.Contracts;
using EmployeeDirectory.Services;
using EmployeeDirectory.Concerns;
using EmployeeDirectory.UI;
using System.Data.Common;
public class Program
{
    static ServiceProvider? serviceProvider;

    static void Main()
    {
        Initialize();
    }

    static void Initialize()
    {
        serviceProvider = Configure();
        ManagementInitialize();
    }

    public static void ManagementInitialize()
    {

        Console.WriteLine(Menus.ManagementMenu);
        int option;
        Utility.GetOption(out option, 3);

        switch ((MainMenu)option)
        {
            case MainMenu.Employee:
                serviceProvider?.GetRequiredService<IEmployeeDirectory>().EmployeeInitalize();
                break;

            case MainMenu.Role:
                serviceProvider?.GetRequiredService<IRoleDirectory>().RoleInitialize();
                break;

            case MainMenu.Exit:
                Environment.Exit(0);
                break;
        }

        ManagementInitialize();
    }

    static ServiceProvider Configure()
    {
        return new ServiceCollection()
                .AddSingleton<IEmployeeService, EmployeeService>()
                .AddSingleton<IRoleService, RoleService>()
                .AddSingleton<IDbConnectionLocal, EmployeeDirectory.Database.DbConnection>()
                .AddSingleton<IEmployeeDirectory, EmployeeDirectory.UI.EmployeeDirectory>()
                .AddSingleton<IRoleDirectory, RoleDirectory>()
                .AddSingleton<IDatabaseServices, DatabaseServices>()
                .BuildServiceProvider();
    }

}