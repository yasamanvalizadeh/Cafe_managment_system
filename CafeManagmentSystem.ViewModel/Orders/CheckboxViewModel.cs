using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeManagementSystem.ViewModel.Orders
{
    public class CheckboxViewModel
    {
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public decimal UnitPrice { get; set; }
        public int CategoryId { get; set; }
        public int Qty { get; set; }
        public bool Checked { get; set; }
    }
}
