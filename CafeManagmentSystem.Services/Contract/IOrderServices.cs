using CafeManagementSystem.ViewModel.Orders;
using CafeSystemManagement.Entities; 

namespace CafeManagmentSystem.Services.Contract
{
    public interface IOrderServices: IGenericServices<Order>
    {
        List<CheckboxViewModel> CreateCheckBoxItem(IEnumerable<Item> itemsList);
    }
}
