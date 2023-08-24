using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GetEmpStatus.MyClasses
{
    public class EmpInfo
    {
        public int UserID { get; set; }
        public string Username { get; set; }
        public int NationalNumber { get; set; }
        public decimal Salary { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int Year { get; set; }
        public String Month { get; set; }
        public decimal AvgSalary { get; set; }
        public decimal LargestSalary { get; set; }
        public string UserSalaryStatus { get; set; }

        public bool IsActive { get; set; }
    }
}
