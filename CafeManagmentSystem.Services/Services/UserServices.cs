using CafeManagementSystem.DataLayer;
using CafeManagementSystem.ViewModel.Users;
using CafeManagmentSystem.Services.Contract; 
using CafeManagmentSystem.ViewModel.Users;
using CafeSystemManagement.Entities;
using Microsoft.EntityFrameworkCore; 

namespace CafeManagmentSystem.Services.Services
{
    public class UserServices : GenericServices<User>,IUserServices
    {
        public UserServices(DataContext db)
            : base(db) { }

        public SortOrderViewModel CreateSortOrderUser(string so, string query)
        {
            var model = new SortOrderViewModel()
            {
                NameSort = so == "UserName_desc" ? "UserName" : "UserName_desc",
                AddDateSort = so == "AddedDate" ? "AddedDate_desc" : "AddedDate",
                UpDateSort = so == "UpdateDate" ? "UpdateDate_desc" : "UpdateDate",
                CurrentFilter = query,
                CurrentSort = so
            };
            return model;
        }
        public User CreateNewUser(AddUserViewModel model)
        {
            var newUser = new User()
            {
                UserName = model.UserName,
                UserNumber = model.phoneNumber,
                UserPassword = model.password,
                AddedDate = DateTime.Now,
                UpdateDate = DateTime.Now,
                FirstName = model.Fname,
                LastName = model.Lname,
                TenantId = model.TenantId
            };
            return newUser;
        }
        public UpdateUserViewModel UpdateUser(User targetUser)
        {
            var model = new UpdateUserViewModel()
            {
                userId = targetUser.UserId,
                UserName = targetUser.UserName,
                phoneNumber = targetUser.UserNumber,
                password = targetUser.UserPassword,
                Fname = targetUser.FirstName,
                Lname = targetUser.LastName,
                RowVersion = targetUser.RowVersion
            };
            return model;
        }

        public IEnumerable<UserSignUpDateGroupViewModel> GetSignUpDateGrouping(IEnumerable<User> users)
        {
            var userGroup = users.GroupBy(User =>User.AddedDate);
            var userL = new List<UserSignUpDateGroupViewModel>();

            foreach(var group in userGroup)
            {
                var model = new UserSignUpDateGroupViewModel()
                {
                    SignUpDate = group.Key,
                    UserCount = group.Count()
                };
                userL.Add(model);
            }

            return userL;
        }
    }
}
