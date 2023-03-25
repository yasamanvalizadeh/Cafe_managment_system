using CafeSystemManagement.Entities;
using System.ComponentModel.DataAnnotations; 
using CafeManagmentSystem.Common.CommonExtensionMethods;

namespace CafeManagmentSystem.ViewModel
{
    public class SortOrderItemViewModel
    {
        [Display(Name = "Item Name")]
        public string NameSort { get; set; }

        [Display(Name = "Item Category")]
        public string CategorySort { get; set; }

        [Display(Name = "Item Price")]
        public string PriceSort { get; set; }

        [Display(Name = "Registered Date")]
        public string AddDateSort { get; set; }

        [Display(Name = "Last Update Date")]
        public string UpDateSort { get; set; }
        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }
        public PaginatedList<Item> Items { get; set; }
    }
}
