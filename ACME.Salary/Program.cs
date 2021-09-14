using System;
using System.Collections.Generic;
using ACME.Salary.Domain;
using ACME.Salary.Services.Salary;
using Microsoft.Extensions.DependencyInjection;

namespace ACME.Salary
{
    class Program
    {

        private static IServiceProvider serviceProvider;
        static void Main(string[] args)
        {

            ConfigureServices();

            var employeeSalary = serviceProvider.GetService<ISalaryServices>();

            employeeSalary.GetSalary().ForEach(x => { Console.WriteLine(x); });

            Console.ReadKey();

            DisposeServices();
        }


        private static void ConfigureServices()
        {
            var services = new ServiceCollection();
            services.AddTransient<ISalaryServices, SalaryService>();

            serviceProvider = services.BuildServiceProvider();
        }

        private static void DisposeServices()
        {
            switch (serviceProvider)
            {
                case null:
                    return;
                case IDisposable disposable:
                    disposable.Dispose();
                    break;
            }
        }
    }
    
}
