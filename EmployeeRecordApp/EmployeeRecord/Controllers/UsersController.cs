using EmployeeRecord.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace EmployeeRecord.Controllers
{
    public class UsersController : Controller
    {
        // GET: Users
        public ActionResult SignUp()
        {
            ViewBag.EmailExist = false;

            return View();
        }

        [NonAction]
        public bool IsUserExist(string UserName)
        {
            using (BajraTaskEntities entities = new BajraTaskEntities())
            {
                var v = entities.Users.Where(a => a.UserName == UserName).FirstOrDefault();
                if (v == null)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }
        [HttpPost]
        public ActionResult SignUp(User usr)
        {
            bool Status = false;
            string message = "";
            if (ModelState.IsValid)
            {
                var isExist = IsUserExist(usr.UserName);
                if (isExist)
                {
                    ModelState.AddModelError("UserExist", "User already exist");
                    ViewBag.EmailExist = true;
                    return View(usr);
                }
                usr.Password = Crypto.Hash(usr.Password);
                usr.ConfirmPassword = Crypto.Hash(usr.ConfirmPassword);

                using (BajraTaskEntities entity = new BajraTaskEntities())
                {
                    entity.Users.Add(usr);
                    entity.SaveChanges();
                    Status = true;
                    message = "Registration completed";
                }

            }
            else
            {
                message = "Invalid Request";
            }
            ViewBag.Message = message;
            ViewBag.Status = Status;

            return View(usr);
        }

        public ActionResult Login(string ReturnUrl)
        {
            TempData["ReturnUrl"] = ReturnUrl;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Login usrLogin, string ReturnUrl)
        {
            string message = "";

            using (BajraTaskEntities entities = new BajraTaskEntities())
            {
                var v = entities.Users.Where(a => a.UserName == usrLogin.UserName).FirstOrDefault();
                if (v != null)
                {
                    if (string.Compare(Crypto.Hash(usrLogin.Password), v.Password) == 0)
                    {
                        int timeout = usrLogin.Remember ? 20 : 5; // 525600 time is 1 year shown in minute
                        var ticket = new FormsAuthenticationTicket(usrLogin.UserName, usrLogin.Remember, timeout);
                        string encrypted = FormsAuthentication.Encrypt(ticket);
                        var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encrypted);
                        cookie.Expires = DateTime.Now.AddDays(timeout);
                        cookie.Secure = true;
                        Response.Cookies.Add(cookie);
                        if (Url.IsLocalUrl(ReturnUrl))
                        {
                            return Redirect(ReturnUrl);
                        }
                        else
                        {
                            return RedirectToAction("GetAllEmployee", "Employee");
                        }


                    }
                    else
                    {
                        message = "Invalid credential Provided";
                    }

                }
                else
                {
                    message = "Invalid Credential provider";
                }
            }
            ViewBag.Message = message;

            return View();
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Users");
        }

    }
}