using CafeManagementSystem.DataLayer;
using CafeManagementSystem.ViewModel.Orders;
using CafeManagmentSystem.Services.Contract;
using CafeSystemManagement.Entities; 

namespace CafeManagmentSystem.Services.Services
{
    public class OrderDetailServices : GenericServices<OrderDetail> , IOrderDetailServices
    {
        public OrderDetailServices(DataContext db)
            : base(db) { }

        public OrderDetail CreateNewOrderDetail(CheckboxViewModel item, Order newOrder)
        {
            var OrderDetailObj = new OrderDetail()
            {
                ItemId = item.ItemId,
                Qty = item.Qty,
                Amount = item.UnitPrice * item.Qty,
                OrderId = newOrder.OrderId,
                AddedDate = newOrder.AddedDate
            };
            return OrderDetailObj;
        }
    }
}
