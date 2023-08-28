using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GetEmpStatus.MyClasses
{
    public class EmpInfo
    {
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "UserID must be a positive integer.")]
        public int UserID { get; set; }

        [StringLength(15, MinimumLength = 1)]
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
