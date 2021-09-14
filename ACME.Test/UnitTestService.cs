using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using ACME.Salary.Domain;
using ACME.Salary.Services.Salary;

namespace ACME.Test
{
    [TestClass]
    public class UnitTestService
    {
        SalaryService SalaryService = new SalaryService();

        [TestMethod]
        public void TestDivideHours()
        {
            List<string> dataExpected = new List<string> { "10:00", "12:00" };
            List<string> dataActual = SalaryService.DivideHours("MO10:00-12:00");
            
            Equals(dataExpected, dataActual);
        }

        [TestMethod]
        public void TestDivideStrings()
        {
            List<string> dataExpected = new List<string> 
            { 
                "RENE", "MO10:00-12:00", "TU10:00-12:00", "TH01:00-03:00", "SA14:00-18:00", "SU20:00-21:00"
            };
            List<string> dataActual = SalaryService.DivideHours("RENE=MO10:00-12:00,TU10:00-12:00,TH01:00-03:00,SA14:00-18:00,SU20:00-21:00");

            Equals(dataExpected, dataActual);
        }

        [TestMethod]
        public void TestReadFile()
        {
            List<string> dataExpected = new List<string>
            {
                "RENE=MO10:00-12:00,TU10:00-12:00,TH01:00-03:00,SA14:00-18:00,SU20:00-21:00",
                "ASTRID=MO10:00-12:00,TH12:00-14:00,SU20:00-21:00",
                "ANDRES=MO12:00-16:00,TU10:00-12:00,TH01:00-03:00,SA13:00-18:00,SU20:00-22:00",
                "KATTY=TU10:00-12:00,WE12:00-15:00,SA13:00-18:00",
                "FERNANDO=FR02:00-03:00,SA15:00-18:00,SU20:00-22:00"
            };
            List<string> dataActual = SalaryService.ReadFile("test");

            Equals(dataExpected, dataActual);
        }

        [TestMethod]
        public void TestGetAmount()
        {
            decimal amountExpected = (decimal) 30.00;
            decimal amountActual = SalaryService.GetAmount(SalaryService.EnterWorkingHours(),"MO10:00-12:00");

            Equals(amountExpected, amountActual);
        }

        [TestMethod]
        public void TestTotalAmountPerEmployee()
        {
            decimal totalExpected = (decimal) 215.00;
            decimal totalActual = (decimal) 0.00;
            List<string> employee = SalaryService.DivideHours("RENE=MO10:00-12:00,TU10:00-12:00,TH01:00-03:00,SA14:00-18:00,SU20:00-21:00");
            List<string> dataActual = SalaryService.DivideStrings(employee[0]);

            for (int i = 1; i < 1; i++)
            {
                totalActual = SalaryService.GetAmount(SalaryService.EnterWorkingHours(), dataActual[i]);
            }
            
            Equals(totalExpected, totalActual);
        }

        [TestMethod]
        public void TestTotalAmount()
        {
            Dictionary<string, List<WorkingHours>> weekHours = SalaryService.EnterWorkingHours();
            decimal totalExpected = (decimal) 900.00;
            decimal totalActual = (decimal) 0.00;
            List<string> employees = new List<string>
            {
                "RENE=MO10:00-12:00,TU10:00-12:00,TH01:00-03:00,SA14:00-18:00,SU20:00-21:00",
                "ASTRID=MO10:00-12:00,TH12:00-14:00,SU20:00-21:00",
                "ANDRES=MO12:00-16:00,TU10:00-12:00,TH01:00-03:00,SA13:00-18:00,SU20:00-22:00",
                "KATTY=TU10:00-12:00,WE12:00-15:00,SA13:00-18:00",
                "FERNANDO=FR02:00-03:00,SA15:00-18:00,SU20:00-22:00"
            };

            foreach (var employee in employees)
            {
                decimal total = (decimal)0.00;
                List<string> campos = SalaryService.DivideStrings(employee);
                for (int i = 1; i < campos.Count; i++)
                {
                    total += SalaryService.GetAmount(weekHours, campos[i]);
                }
                totalActual += total;
            }

            Equals(totalExpected, totalActual);
        }

        [TestMethod]
        public void TestGetSalary()
        {
            List<string> totalExpected = new List<string>
            {
                "The amount to pay RENE is: 215,00 USD",
                "The amount to pay ASTRID is: 85,00 USD",
                "The amount to pay ANDRES is: 290,00 USD",
                "The amount to pay KATTY is: 175,00 USD",
                "The amount to pay FERNANDO is: 135,00 USD"
            };

            List<string> totalActual = SalaryService.GetSalary();

            Equals(totalExpected, totalActual);
        }
    }
}
