using CafeManagementSystem.ViewModel.Users;
using CafeManagmentSystem.ViewModel.Users;
using CafeSystemManagement.Entities; 

namespace CafeManagmentSystem.Services.Contract
{
    public interface IUserServices : IGenericServices<User>
    {
        SortOrderViewModel CreateSortOrderUser(string so, string query);
        User CreateNewUser(AddUserViewModel model);
        UpdateUserViewModel UpdateUser(User model);
        IEnumerable<UserSignUpDateGroupViewModel> GetSignUpDateGrouping(IEnumerable<User> users);
    }
}
