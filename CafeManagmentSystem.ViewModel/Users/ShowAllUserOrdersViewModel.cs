using CafeSystemManagement.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeManagmentSystem.ViewModel.Users
{
    public class ShowAllUserOrdersViewModel
    {
        [Display(Name ="Full Name")]
        public string FullName { get; set; }
        public ICollection<Order> Orders { get; set; } 
    }
}
