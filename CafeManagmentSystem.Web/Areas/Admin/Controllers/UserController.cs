using CafeManagementSystem.Common.AttributesErrorMessageCommon;
using CafeManagementSystem.Commons; 
using CafeManagementSystem.ViewModel.Users;
using CafeManagmentSystem.Common;
using CafeManagmentSystem.Common.CommonExtensionMethods;
using CafeManagmentSystem.Services.UnitOfWork;
using CafeManagmentSystem.ViewModel.Users;
using CafeSystemManagement.Entities; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.EntityFrameworkCore; 

namespace CafeManagementSystem.Areas.Admin.Controllers
{
    [Area(AreaAttributs.AreaAdmin)]
    public class UserController : Controller
    {
        private readonly IUnitOfWork _uow;
        private readonly ILogger<UserController> _logger;
        private readonly IConfiguration _configuration; 

        public UserController(IUnitOfWork uow,
            ILogger<UserController> logger,
            IConfiguration configuration )
        {
            _uow = uow;
            _logger = logger;
            _configuration = configuration; 
        }

        public async Task<IActionResult> Index(string so, string query, int? pageIndex)
        {
            IEnumerable<User> usersIQ;

            bool descending = false;
            string nso = "";

            if (string.IsNullOrEmpty(so))
                so = "UserName";

            if (string.IsNullOrEmpty(query))
                query = "";

            if (!string.IsNullOrEmpty(query))
                pageIndex = 1;  

            if (so.EndsWith("_desc"))
            {
                nso = so.Substring(0, so.Length - 5);
                descending = true;
            }

            if (_uow.tenantId != -1)
                ViewData["CurrentTenantId"] = _uow.tenantId;

            var pageSize = _configuration.GetValue(PageSizeCommon.PageSizeNum, 4);
            var model = _uow.UserServices.CreateSortOrderUser(so, query);

            if (descending)
            {
                usersIQ = await _uow.UserServices.GetAllAsync(filter: u => u.UserName.Contains(query), OrderByDescending: e => EF.Property<object>(e, nso), tenantId: _uow.tenantId);
                model.Users = await PaginatedList<User>.CreateAsync( usersIQ, pageIndex ?? 1, pageSize);

                return View(model);
            }
            else
            {
                usersIQ = await _uow.UserServices.GetAllAsync(filter: u => u.UserName.Contains(query), orderBy: e => EF.Property<object>(e, so), tenantId: _uow.tenantId);
                model.Users = await PaginatedList<User>.CreateAsync(usersIQ,pageIndex ?? 1, pageSize);
                 
                return View(model);
            }
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(AddUserViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (model == null)
                        return BadRequest();

                    var newUser = _uow.UserServices.CreateNewUser(model);

                    await _uow.UserServices.AddAsync(newUser);
                    await _uow.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch(DbUpdateException ex)
            {
                _logger.LogError(ex.Message);
                ModelState.AddModelError(string.Empty, ErrorMessageCommon.ModelStateErrorMessage);
            }
            return View(model);
        }

        public async Task<IActionResult> UpdateUser(int id, string so, string query,int pageIndex, int TenantId)
        {
            if (id == null)
                return NotFound();

            var targetUser = await _uow.UserServices.FindByIdAsync(id);

            if (targetUser == null)
                return NotFound();

            var model = _uow.UserServices.UpdateUser(targetUser);

            ViewData["CurrentSort"] = so;
            ViewData["CurrentFilter"] = query;
            ViewData["CurrentPageIndex"] = pageIndex;
            ViewData["CurrentTenantId"] = TenantId;

            return View(model);
        }
        
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateUser(UpdateUserViewModel model, string so, string? query, int pageIndex , int TenantId)
        {
            if (ModelState.IsValid)
            {
                if(model == null)
                    return BadRequest();

                var targetUser = await _uow.UserServices.FindByIdAsync(model.userId);

                if (targetUser == null)
                    return NotFound();

                var currentUserPass = targetUser.UserPassword;
                
                targetUser.UserName = model.UserName;
                targetUser.UserNumber = model.phoneNumber;
                targetUser.UpdateDate = DateTime.Now;
                targetUser.FirstName = model.Fname;
                targetUser.LastName = model.Lname; 

                if (model.password is null)
                    targetUser.UserPassword = currentUserPass;
                else
                    targetUser.UserPassword = model.password;
                
                targetUser.RowVersion = model.RowVersion; 

                try
                {
                    await _uow.SaveChangesAsync();
                    return RedirectToAction("Index", new { so = so, query = query, pageIndex = pageIndex});
                }
                catch(DbUpdateConcurrencyException ex)
                {
                    var exceptionEntry = ex.Entries.Single();
                    var clientValues = (User)exceptionEntry.Entity;
                    var databaseEntry = exceptionEntry.GetDatabaseValues();

                    if(databaseEntry == null)
                    {
                        ModelState.AddModelError(string.Empty, ErrorMessageCommon.NullDatabaseUserEntityErrorMessage);
                    }
                    else
                    {
                        var databaseValues = (User)databaseEntry.ToObject();
                        if(databaseValues.FirstName != clientValues.FirstName)
                            ModelState.AddModelError("FirstName", $"Current value: {databaseValues.FirstName}");
                        if(databaseValues.LastName != clientValues.LastName)
                            ModelState.AddModelError("LastName", $"Current value: {databaseValues.LastName}");
                        if(databaseValues.UserName != clientValues.UserName)
                            ModelState.AddModelError("UserName", $"Current value: {databaseValues.UserName}");
                        if(databaseValues.UserNumber != clientValues.UserNumber)
                            ModelState.AddModelError("UserNumber", $"Current value: {databaseValues.UserNumber}");
                        if(databaseValues.UserPassword != clientValues.UserPassword)
                            ModelState.AddModelError("UserPassword", $"Current value: {databaseValues.UserPassword}");

                        ModelState.AddModelError(string.Empty, "The record you attempted to edit "
                          + "was modified by another user after you got the original value. The "
                          + "edit operation was canceled and the current values in the database "
                          + "have been displayed. If you still want to edit this record, click "
                          + "the Save button again. Otherwise click the Back to List hyperlink.");

                        model.RowVersion = databaseValues.RowVersion;
                        ModelState.Remove("RowVersion");

                        ViewData["CurrentSort"] = so;
                        ViewData["CurrentFilter"] = query;
                        ViewData["CurrentPageIndex"] = pageIndex;
                        ViewData["CurrentTenantId"] = TenantId;

                        return View(model);
                    }
                }
            }
            else 
                ModelState.AddModelError(string.Empty, ErrorMessageCommon.ModelStateErrorMessage); 
 
            return View(model);
        }
        
        public async Task<IActionResult> DeleteUser(int id, string so, string query, int pageIndex, bool? concurrencyError, int TenantId)
        {
            if (id == null)
                return NotFound();

            var targetUser = await _uow.UserServices.FindByIdAsync(id);

            if(targetUser == null)
            {
                if (concurrencyError.GetValueOrDefault())
                    return RedirectToAction(nameof(Index), new { so = so, query = query, pageIndex = pageIndex });
                
                return NotFound();
            }

            if (concurrencyError.GetValueOrDefault())
            {
                ViewData["ConcurrencyErrorMessage"] = "The record you attempted to delete "
                   + "was modified by another user after you got the original values. "
                   + "The delete operation was canceled and the current values in the "
                   + "database have been displayed. If you still want to delete this "
                   + "record, click the Delete button again. Otherwise "
                   + "click the Back to List hyperlink.";
            } 

            ViewData["CurrentSort"] = so;
            ViewData["CurrentFilter"] = query;
            ViewData["CurrentPageIndex"] = pageIndex;
            ViewData["CurrentTenantId"] = TenantId;

            return View(targetUser);
            
        }
        
        [HttpPost,ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteUser(User model , string so, string? query, int pageIndex , int TenantId)
        { 
            try
            {
                if(await _uow.UserServices.Any(u => u.UserId == model.UserId))
                {
                    _uow.UserServices.Remove(model);
                    await _uow.SaveChangesAsync();
                } 
                return RedirectToAction("Index", new { so = so, query = query , pageIndex = pageIndex});
            } 
            catch (DbUpdateConcurrencyException ex)
            { 
                _logger.LogError(ex.Message, $"Failed to delete user with Id: {model.UserId}");
                return RedirectToAction(nameof(DeleteUser), new { id = model.UserId, concurrencyError = true, so = so, query = query, pageIndex = pageIndex , TenantId = TenantId});
            } 
        }

        public async Task<IActionResult> SignUpDateGroping()
        {
            var users = await _uow.UserServices.GetAllAsync(); 
            var SignUpGroup = _uow.UserServices.GetSignUpDateGrouping(users); 
            return View(SignUpGroup);
        }

        public async Task<IActionResult> ShowUserOrders(int id, string so, string query, int pageIndex)
        {
            var user = await _uow.UserServices.GetAllAsync(filter: u => u.UserId == id, includeProperties: "UserOrders.OrderDetail.Item");
            var targetUser = user.FirstOrDefault();
             

            if (id != null)
            {  
                var UserOrdersDetail = new ShowAllUserOrdersViewModel()
                {
                    FullName = $"{targetUser.FirstName} {targetUser.LastName}",
                    Orders = targetUser.UserOrders
                };

                ViewData["CurrentSort"] = so;
                ViewData["CurrentFilter"] = query;
                ViewData["CurrentPageIndex"] = pageIndex;

                return View(UserOrdersDetail);
            }
            else
                return BadRequest();
        }
    }
}
