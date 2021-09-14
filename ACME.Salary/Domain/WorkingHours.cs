using System;
using System.Collections.Generic;
using System.Text;

namespace ACME.Salary.Domain
{
    public class WorkingHours
    {
        public DateTime startTime { get; set; }
        public DateTime endTime{ get; set; }
        public decimal salary { get; set; }

        public WorkingHours(DateTime _startTime, DateTime _endTime, decimal _salary)
        {
            this.startTime = _startTime;
            this.endTime = _endTime;
            this.salary = _salary;
        }


    }
}
