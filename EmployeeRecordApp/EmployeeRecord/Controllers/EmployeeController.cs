using EmployeeRecord.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace EmployeeRecord.Controllers
{
    [Authorize]
    public class EmployeeController : Controller
    {
        // GET: Employee
        BajraTaskEntities entities = new BajraTaskEntities();
        public ActionResult GetAllEmployee()
        {
            var Employees = entities.EmployeeDatas.ToList();
            DateTime today = DateTime.Now;
            decimal salarySum = 0; 
            decimal totalSalSum = 0;
            
            foreach(var item in Employees)
            {
                var joinedDate = (item.JoinDate != null)?item.JoinDate:DateTime.Now;
                var diff = today.Subtract((DateTime)joinedDate);
                var totalWorkedMonth = diff.Days / 30;
                item.TotalWorkedMonth = totalWorkedMonth;
                item.TotalSalaryEarned = (decimal)(totalWorkedMonth * item.MonthlySalary);
                salarySum = (decimal)(salarySum + item.MonthlySalary);
                totalSalSum = totalSalSum + item.TotalSalaryEarned;
            }
            ViewBag.MonthlySalarySum = salarySum;
            ViewBag.TotalSalSum = totalSalSum;
            

            return View(Employees);
        }

        

        public ActionResult AddEmployee()
        {
            ViewBag.heading = "Add Employee";
            return View();
        }

        [HttpPost]
        public ActionResult AddEmployee([Bind(Exclude = "EmployeeId,TotalWorkedMonth,TotalSalaryEarned")]EmployeeData emp)
        {
            bool status = false;
            string message = "";
            if (ModelState.IsValid)
            {
                using (BajraTaskEntities enitites = new BajraTaskEntities())
                {
                    enitites.EmployeeDatas.Add(emp);
                    enitites.SaveChanges();
                    message = "Employee sucessfully Added";
                    status = true;
                }

            }
            else
            {
                message = "Employee Creation Failed";

            }
            ViewBag.Message = message;
            ViewBag.Status = status;

            return View();
        }

        public ActionResult EditEmployee(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmployeeData EmpData = entities.EmployeeDatas.Find(id);
            if (EmpData == null)
            {
                return HttpNotFound();
            }
            ViewBag.heading = "Edit Employee";
            return View("AddEmployee", EmpData);
            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditEmployee(EmployeeData emp)
        {
            if (ModelState.IsValid)
            {
                entities.Entry(emp).State = EntityState.Modified;
                try
                {
                    entities.SaveChanges();
                }
                catch(DbUpdateConcurrencyException ex)
                {
                    ex.Entries.Single().Reload();
                    
                }
                
                return RedirectToAction("GetAllEmployee");
            }
            ViewBag.message = "update Failed";
            return View("AddEmployee", emp);

        }

        
        public string DeleteEmployee(int? id)
        {
            if(id!=null)
            {
                EmployeeData emp = entities.EmployeeDatas.Find(id);
                entities.EmployeeDatas.Remove(emp);

                entities.SaveChanges();
                return "Record Sucessfully Deleted";
            }
            
            return "Failed To delete Record";
        }


    }
}