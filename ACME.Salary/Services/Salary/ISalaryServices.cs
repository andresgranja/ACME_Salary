using ACME.Salary.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace ACME.Salary.Services.Salary
{
    public interface ISalaryServices
    {
        Dictionary<string, List<WorkingHours>> EnterWorkingHours();
        List<string> DivideStrings(string cadena);
        List<string> DivideHours(string cadena);
        List<string> ReadFile(string env);
        decimal GetAmount(Dictionary<string, List<WorkingHours>> workingHours, string day);
        List<string> GetSalary();
    }
}
