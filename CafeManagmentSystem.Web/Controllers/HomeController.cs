
using CafeManagementSystem.Common.AttributesErrorMessageCommon; 
using CafeManagementSystem.ViewModel.Users;
using CafeManagmentSystem.Services.UnitOfWork; 
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore; 

namespace CafeManagementSystem.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _uow;

        public HomeController(ILogger<HomeController> logger ,IUnitOfWork uow)
		{
			_logger = logger;
			_uow = uow;
		}

		public IActionResult Index()
		{ 
            return View();
		}

		public IActionResult SignUp()
		{
			return View();
		}

		[HttpPost, ValidateAntiForgeryToken]
		public async Task<IActionResult> SignUp(AddUserViewModel model)
		{
			try
			{
                if (ModelState.IsValid)
                {
					var newUser = _uow.UserServices.CreateNewUser(model);

                    await _uow.UserServices.AddAsync(newUser);
                    await _uow.SaveChangesAsync();
                    return Json("succuss");
                }
            }
			catch(DbUpdateException ex)
			{
				_logger.LogError(ex.Message);
				ModelState.AddModelError(string.Empty, ErrorMessageCommon.ModelStateErrorMessage);
			}
			return View(model);
		}
		 
        public IActionResult LogOut() 
		{
			return View();
		}
    }
}