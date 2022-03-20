﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EmployeeRecord.Models
{
    public class Login
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "UserName Required")]
        [Display(Name = "User Name")]
        public string UserName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Password Required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }


        [Display(Name = "Remember Me")]
        public bool Remember { get; set; }
    }
}