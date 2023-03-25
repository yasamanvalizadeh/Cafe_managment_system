using CafeManagementSystem.ViewModel.Orders;
using CafeSystemManagement.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeManagmentSystem.Services.Contract
{
    public interface IOrderDetailServices : IGenericServices<OrderDetail>
    {
        OrderDetail CreateNewOrderDetail(CheckboxViewModel item, Order newOrder);
    }
}
