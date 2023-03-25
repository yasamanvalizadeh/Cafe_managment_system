using CafeManagementSystem.Common.AttributesErrorMessageCommon; 
using CafeManagementSystem.ViewModel.Orders;
using CafeManagmentSystem.Services.UnitOfWork;
using CafeSystemManagement.Entities;
using Microsoft.AspNetCore.Mvc; 

namespace CafeManagementSystem.Controllers
{
    public class UserController : Controller
    {
        private readonly IUnitOfWork _uow;
        public UserController(IUnitOfWork uow)
        {
            _uow = uow;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> MakeOrder()
        {
            var model = new MakeOrderViewModel();
            
            var Items = await _uow.ItemServices.GetAllAsync();
            var items = _uow.OrderServices.CreateCheckBoxItem(Items);
            model.items = items;
            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> MakeOrders(MakeOrderViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model == null)
                    return BadRequest();

                var users = await _uow.UserServices.GetAllAsync(filter: u => u.UserName.Equals(model.UserName));
                var targetUserName = users.SingleOrDefault();
                
                if (targetUserName == null)
                {
                    TempData["UserNotFound"] = ErrorMessageCommon.UserNotFound;
                    return RedirectToAction(nameof(MakeOrder));
                }
                else
                {
                    if (targetUserName.UserPassword == model.password)
                    {
                        var newOrder = new Order()
                        {
                            AddedDate = DateTime.Now, 
                            UserId = targetUserName.UserId
                        };
                        await _uow.OrderServices.AddAsync(newOrder);
                        await _uow.SaveChangesAsync();

                        foreach (var item in model.items)
                        {
                            if (item.Checked)
                            {
                                var OrderDetailObj = _uow.OrderDetailServices.CreateNewOrderDetail(item, newOrder);
                                await _uow.OrderDetailServices.AddAsync(OrderDetailObj); 
                            }
                        }
                        await _uow.SaveChangesAsync();
                        return RedirectToAction(nameof(ManageOrders), new { UserId=targetUserName.UserId});
                    }
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, ErrorMessageCommon.ModelStateErrorMessage);
                return View(nameof(MakeOrder));
            }
            return View(nameof(MakeOrder));
        }

        public async Task<IActionResult> ManageOrders(int UserId)
        {
            var orders = await _uow.OrderServices.GetAllAsync(filter: o => o.UserId == UserId, includeProperties:"User");
            foreach(var o in orders)
            {
                ViewBag.OrderDetails = await _uow.OrderDetailServices.GetAllAsync(filter: o => o.OrderId == o.OrderId, includeProperties: "Item");
            }
              
            if (ViewBag.OrderDetails == null)
            {
                return NotFound();
            }
            else
                return View(orders);
        }
    }
}
