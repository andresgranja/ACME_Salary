using ACME.Salary.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;

namespace ACME.Salary.Services.Salary
{
    public class SalaryService : ISalaryServices
    {
        public Dictionary<string, List<WorkingHours>> EnterWorkingHours()
        {
            Dictionary<string, List<WorkingHours>> weekHours = new Dictionary<string, List<WorkingHours>>();
            List<WorkingHours> weekDays = new List<WorkingHours>();
            List<WorkingHours> weekEnd = new List<WorkingHours>();
            WorkingHours hours1 = new WorkingHours(DateTime.Parse("00:01", CultureInfo.InvariantCulture, DateTimeStyles.NoCurrentDateDefault), DateTime.Parse("09:00", CultureInfo.InvariantCulture, DateTimeStyles.NoCurrentDateDefault), (decimal) 25.00);
            WorkingHours hours2 = new WorkingHours(DateTime.Parse("09:01", CultureInfo.InvariantCulture, DateTimeStyles.NoCurrentDateDefault), DateTime.Parse("18:00", CultureInfo.InvariantCulture, DateTimeStyles.NoCurrentDateDefault), (decimal) 15.00);
            WorkingHours hours3 = new WorkingHours(DateTime.Parse("18:01", CultureInfo.InvariantCulture, DateTimeStyles.NoCurrentDateDefault), DateTime.Parse("23:59", CultureInfo.InvariantCulture, DateTimeStyles.NoCurrentDateDefault), (decimal) 20.00);
            WorkingHours hours4 = new WorkingHours(DateTime.Parse("00:01", CultureInfo.InvariantCulture, DateTimeStyles.NoCurrentDateDefault), DateTime.Parse("09:00", CultureInfo.InvariantCulture, DateTimeStyles.NoCurrentDateDefault), (decimal) 30.00);
            WorkingHours hours5 = new WorkingHours(DateTime.Parse("09:01", CultureInfo.InvariantCulture, DateTimeStyles.NoCurrentDateDefault), DateTime.Parse("18:00", CultureInfo.InvariantCulture, DateTimeStyles.NoCurrentDateDefault), (decimal) 20.00);
            WorkingHours hours6 = new WorkingHours(DateTime.Parse("18:01", CultureInfo.InvariantCulture, DateTimeStyles.NoCurrentDateDefault), DateTime.Parse("23:59", CultureInfo.InvariantCulture, DateTimeStyles.NoCurrentDateDefault), (decimal) 25.00);

            weekDays.Add(hours1);
            weekDays.Add(hours2);
            weekDays.Add(hours3);

            weekEnd.Add(hours4);
            weekEnd.Add(hours5);
            weekEnd.Add(hours6);

            weekHours.Add(Week.WeekDays.MO.ToString(), weekDays);
            weekHours.Add(Week.WeekDays.TU.ToString(), weekDays);
            weekHours.Add(Week.WeekDays.WE.ToString(), weekDays);
            weekHours.Add(Week.WeekDays.TH.ToString(), weekDays);
            weekHours.Add(Week.WeekDays.FR.ToString(), weekDays);
            weekHours.Add(Week.WeekDays.SA.ToString(), weekEnd);
            weekHours.Add(Week.WeekDays.SU.ToString(), weekEnd);

            return weekHours;
        }
        public List<string> DivideHours(string cadena)
        {
            return cadena.Split("-").ToList();
        }

        public List<string> DivideStrings(string cadena)
        {
            return Regex.Split(cadena, @"=|,").ToList();
        }

        public List<string> ReadFile(string env)
        {
            List<string> lines = new List<string>();
            string line = "";
            string filePath = "";
            //string filePath = Path.GetDirectoryName(Path.GetDirectoryName(Path.GetDirectoryName(Directory.GetCurrentDirectory()))) + @"\ACME\" + env + @"\Data\Employees.txt";
            //string filePath = Path.GetDirectoryName(Path.GetDirectoryName(Path.GetDirectoryName(Directory.GetCurrentDirectory()))) + @"ACME.Salary\Data\Employees.txt";
            filePath = env == "salary" ? @"C:\ACME\ACME.Salary\Data\Employees.txt" : @"C:\ACME\ACME.Test\Data\Employees.txt";

            //if (env == "salary")
            //{
            //    filePath = @"C:\ACME\ACME.Salary\Data\Employees.txt";
            //} 
            //else 
            //{ 
            //    filePath = @"C:\ACME\ACME.Test\Data\Employees.txt";
            //}


            StreamReader streamReader = new StreamReader(filePath);
            line = streamReader.ReadLine();
            streamReader.Close();
            if (line == null || line == "" )
            {
                lines.Add("RENE=MO10:00-12:00,TU10:00-12:00,TH01:00-03:00,SA14:00-18:00,SU20:00-21:00");
                lines.Add("ASTRID=MO10:00-12:00,TH12:00-14:00,SU20:00-21:00");
                lines.Add("ANDRES=MO12:00-16:00,TU10:00-12:00,TH01:00-03:00,SA13:00-18:00,SU20:00-22:00");
                lines.Add("KATTY=TU10:00-12:00,WE12:00-15:00,SA13:00-18:00");
                lines.Add("FERNANDO=FR02:00-03:00,SA15:00-18:00,SU20:00-22:00");
                StreamWriter streamWriter = new StreamWriter(filePath, append: true);
                lines.ForEach(x => { streamWriter.WriteLine(x); });
                streamWriter.Close();
            } else {
                streamReader = new StreamReader(filePath);
                line = streamReader.ReadLine();
                while (line != null)
                {
                    lines.Add(line);
                    line = streamReader.ReadLine();
                }
                streamReader.Close();
            }
            
            return lines;
        }

        public decimal GetAmount(Dictionary<string, List<WorkingHours>> workingHours, string day)
        {
            decimal total = (decimal) 0.00;
            List<string> hours = DivideHours(day[2..]);
            List<WorkingHours> dayHours = workingHours.GetValueOrDefault(day.Substring(0, 2));
            List<WorkingHours> dayHoursFiltered = dayHours.Where(x => (hours[0].CompareTo(x.startTime.TimeOfDay.ToString()) >= 0) && (hours[1].CompareTo(x.endTime.TimeOfDay.ToString()) <= 0)).ToList();

            dayHoursFiltered.ForEach(x => {
                int count = Convert.ToInt32(hours[1].Substring(0, 2)) - Convert.ToInt32(hours[0].Substring(0, 2));
                total += x.salary * count; 
            });

            return total;
        }

        public List<string> GetSalary()
        {
            Dictionary<string, List<WorkingHours>> weekHours = EnterWorkingHours();
            List<string> employees = ReadFile("salary");
            List<string> salaries = new List<string>();

            foreach (var employee in employees)
            {
                decimal total = (decimal) 0.00;
                List<string> campos = DivideStrings(employee);
                for (int i = 1; i < campos.Count; i++)
                {
                    total += GetAmount(weekHours, campos[i]);
                }
                salaries.Add("The amount to pay " + campos[0] + " is: " + String.Format("{0:0.00} USD", total));
            }

            return salaries;
        }
    }
}