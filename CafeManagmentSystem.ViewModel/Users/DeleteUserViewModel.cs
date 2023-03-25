using CafeManagementSystem.Common.AttributesErrorMessageCommon;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CafeManagmentSystem.ViewModel.Users
{
    public class DeleteUserViewModel
    {
        [HiddenInput]
        public int userId { get; set; }

        [Display(Name = "User Name")] 
        public string? userName { get; set; }

        [Display(Name = "Phone Number")]
        public string? phoneNumber { get; set; } 
    }
}
