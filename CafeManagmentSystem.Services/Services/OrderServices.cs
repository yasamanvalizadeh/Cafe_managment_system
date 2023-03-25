using CafeManagementSystem.DataLayer;
using CafeManagementSystem.ViewModel.Orders;
using CafeManagmentSystem.Services.Contract;
using CafeSystemManagement.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeManagmentSystem.Services.Services
{
    public class OrderServices : GenericServices<Order> , IOrderServices
    {
        public OrderServices(DataContext  db)
            :base(db) { }

        public List<CheckboxViewModel> CreateCheckBoxItem(IEnumerable<Item> itemsList)
        {
            var items = new List<CheckboxViewModel>();

            foreach (var Item in itemsList)
            {
                var item = new CheckboxViewModel()
                {
                    ItemId = Item.ItemId,
                    ItemName = Item.ItemName,
                    UnitPrice = Item.UnitPrice,
                    CategoryId = Item.CategoryId,
                    Checked = false,
                    Qty = 0
                };

                items.Add(item);
            }

            return items;
        }
    }
}
