using CafeManagementSystem.DataLayer;
using CafeManagementSystem.ViewModel.Orders;
using CafeManagmentSystem.Services.Contract;
using CafeManagmentSystem.ViewModel;
using CafeSystemManagement.Entities; 

namespace CafeManagmentSystem.Services.Services
{
    public class ItemServices : GenericServices<Item>, IitemServices 
    { 
        public ItemServices(DataContext db)
           : base(db) { }

        public SortOrderItemViewModel CreateSortOrderItem(string so, string query)
        {
            var model = new SortOrderItemViewModel()
            {
                NameSort = so == "ItemName_desc" ? "ItemName" : "ItemName_desc",
                CategorySort = so == "Category" ? "Category_desc" : "Category",
                PriceSort = so == "UnitPrice" ? "UnitPrice_desc" : "UnitPrice",
                AddDateSort = so == "AddedDate" ? "AddedDate_desc" : "AddedDate",
                UpDateSort = so == "UpdateDate" ? "UpdateDate_desc" : "UpdateDate",
                CurrentFilter = query,
                CurrentSort = so
            };
            return model;
        }
         
    }
}
