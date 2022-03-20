//task 2
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Employee.Models
{
    public class MappingClass
    {
        public string teamSize { get; set; }
        public string projects { get; set; }
        public string happyClients { get; set; }
        public Company company { get; set; }

        public List<Branch> Branches { get; set; }
    }

    public class Company
    {
        public int CompanyID { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string address { get; set; }
        public string Phone { get; set; }

    }

    public class Branch
    {
        public int branchId { get; set; }
        public string name { get; set; }

        public string address { get; set; }
    }
}