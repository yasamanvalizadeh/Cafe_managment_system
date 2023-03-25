using CafeManagmentSystem.Common.CommonExtensionMethods;
using CafeSystemManagement.Entities; 
using System.ComponentModel.DataAnnotations; 
 

namespace CafeManagmentSystem.ViewModel.Users
{
    public class SortOrderViewModel
    {
        [Display(Name = "User Name")]
        public string NameSort { get; set; }

        [Display(Name = "Registered Date")]
        public string AddDateSort { get; set; }

        [Display(Name = "Last Update Date")]
        public string UpDateSort { get; set; } 
        public string CurrentFilter { get; set; } 
        public string CurrentSort { get; set; }
        public PaginatedList<User> Users { get; set; }
    }
}
