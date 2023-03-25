using CafeManagementSystem.ViewModel.Orders;
using CafeManagmentSystem.ViewModel;
using CafeSystemManagement.Entities; 

namespace CafeManagmentSystem.Services.Contract
{
    public interface IitemServices : IGenericServices<Item>
    {
        SortOrderItemViewModel CreateSortOrderItem(string so, string query); 
    }
}
