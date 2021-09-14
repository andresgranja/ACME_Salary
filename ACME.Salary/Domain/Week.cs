using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace ACME.Salary.Domain
{
    public class Week
    {
        public string Code { get; set; }

        public enum WeekDays
        {
            [Description("MONDAY")] MO,
            [Description("TUESDAY")] TU,
            [Description("WEDNESDAY")] WE,
            [Description("THURSDAY")] TH,
            [Description("FRIDAY")] FR,
            [Description("SATURDAY")] SA,
            [Description("SUNDAY")] SU
        }
    }
}
