using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EmployeeRecord.Models
{
    [MetadataType(typeof(EmployeeMetaData))]
    public partial class EmployeeData
    {
        public int TotalWorkedMonth { get; set; }
        public decimal TotalSalaryEarned { get; set; }
    }

    public class EmployeeMetaData
    {

        public int EmployeeId { get; set; }
        
        [Required(AllowEmptyStrings = false, ErrorMessage = "please enter FirstName")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Middle Name")]
        public string MiddleName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "please enter LastName")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Joining Date")]
        public Nullable<System.DateTime> JoinDate { get; set; }

        [Display(Name = "Monthly Salary")]

        public Nullable<decimal> MonthlySalary { get; set; }

        [Display(Name = "Total Month Workeed")]
        public int TotalWorkedMonth { get; set; }

        [Display(Name ="Total Salary Earned")]
        public decimal TotalSalaryEarned { get; set; }
    }
}