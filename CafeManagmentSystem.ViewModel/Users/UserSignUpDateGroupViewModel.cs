using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeManagmentSystem.ViewModel.Users
{
    public class UserSignUpDateGroupViewModel
    {
 
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy - hh:mm tt}")]
        public DateTime? SignUpDate { get; set; }
         
        public int UserCount { get; set; } 
    }
}
