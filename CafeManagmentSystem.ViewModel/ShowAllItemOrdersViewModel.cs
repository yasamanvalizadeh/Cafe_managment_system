using CafeSystemManagement.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeManagmentSystem.ViewModel
{
    public class ShowAllItemOrdersViewModel
    {
        public ICollection<OrderDetail> OrderDetails { get; set; }  
    }
}
