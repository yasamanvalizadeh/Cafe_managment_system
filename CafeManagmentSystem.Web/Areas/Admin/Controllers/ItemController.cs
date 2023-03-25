using CafeManagementSystem.Common.AttributesErrorMessageCommon;
using CafeManagementSystem.Commons; 
using CafeManagementSystem.ViewModel;
using CafeManagmentSystem.Common;
using CafeManagmentSystem.Common.CommonExtensionMethods;  
using CafeManagmentSystem.Services.UnitOfWork;
using CafeManagmentSystem.ViewModel;
using CafeSystemManagement.Entities;
using Microsoft.AspNetCore.Mvc; 
using Microsoft.EntityFrameworkCore; 

namespace CafeManagementSystem.Areas.Admin.Controllers
{ 
    [Area(AreaAttributs.AreaAdmin)]
    public class ItemController : Controller
    {
        private readonly IUnitOfWork _uow; 
        private readonly ILogger<ItemController> _logger;
        private readonly IConfiguration _configuration;  

        public ItemController(IUnitOfWork dataContext, 
            ILogger<ItemController> logger,
            IConfiguration configuration)
        {
            _uow = dataContext;
            _logger = logger;
            _configuration = configuration; 
        } 
        public async Task<IActionResult> Index(string so, string query, int? pageIndex)
        {
            IEnumerable<Item> itemsIQ;

            bool descending = false;
            string nso = "";

            if (string.IsNullOrEmpty(so))
                so = "ItemName";

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
            var model = _uow.ItemServices.CreateSortOrderItem(so, query);
             
            if (descending)
            { 
                itemsIQ = await _uow.ItemServices.GetAllAsync(includeProperties: "Category", filter: i => i.ItemName.Contains(query), OrderByDescending: e => EF.Property<object>(e, nso), tenantId: _uow.tenantId);
                model.Items = await PaginatedList<Item>.CreateAsync(itemsIQ, pageIndex ?? 1, pageSize);

                return View(model);
            }
            else
            {
                itemsIQ = await _uow.ItemServices.GetAllAsync(includeProperties: "Category", filter: i => i.ItemName.Contains(query), orderBy: e => EF.Property<object>(e, so), tenantId: _uow.tenantId);
                model.Items = await PaginatedList<Item>.CreateAsync(itemsIQ, pageIndex ?? 1, pageSize);
                 
                return View(model);
            }  
        }
        
        public async Task<IActionResult> Add()
        {
            ViewBag.CatsId = _uow.CategoryServices.CategoryDropDownList();
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(AddItemViewModel newItem)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (newItem == null)
                    {
                        return BadRequest();
                    }
                    var targetCat = await _uow.CategoryServices.FindByIdAsync(newItem.CategoryId);

                    var item = new Item()
                    {
                        ItemName = newItem.ItemName,
                        Category = targetCat,
                        UnitPrice = newItem.UnitPrice,
                        AddedDate = DateTime.Now,
                        UpdateDate = DateTime.Now,
                        TenantId = newItem.TenantId
                    };

                    await _uow.ItemServices.AddAsync(item);
                    await _uow.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex.Message);
                ModelState.AddModelError(string.Empty, ErrorMessageCommon.ModelStateErrorMessage);
            }
            return View(newItem);
        }
         
        public async Task<IActionResult> Edit(int id, string so, string query, int pageIndex, int TenantId)
        {
            if (id == null)
            {
                return NotFound();
            }

            var targetItem = await _uow.ItemServices.FindByIdAsync(id); 

            if (targetItem == null)
            {
                return NotFound();
            }

            var model = new EditItemViewModel()
            {
                ItemName = targetItem.ItemName,
                CategoryId = targetItem.CategoryId,
                UnitPrice = targetItem.UnitPrice,
                ItemId = targetItem.ItemId,
                RowVersion = targetItem.RowVersion
            };

            ViewData["CurrentSort"] = so;
            ViewData["CurrentFilter"] = query;
            ViewData["CurrentPageIndex"] = pageIndex;
            ViewData["CurrentTenantId"] = TenantId;

            ViewBag.Cats = await _uow.CategoryServices.GetAllAsync();
            ViewBag.CatsId = _uow.CategoryServices.CategoryDropDownList(targetItem.CategoryId);
            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditItemViewModel newItem, string so, string? query, int pageIndex, int TenantId)
        { 
            if (ModelState.IsValid)
            { 
                if (newItem == null)
                {
                    return BadRequest();
                }

                var item = await _uow.ItemServices.FindByIdAsync(newItem.ItemId);
                if (item == null)
                {
                    return NotFound();
                }


                item.ItemName = newItem.ItemName;
                item.CategoryId = newItem.CategoryId;
                item.UnitPrice = newItem.UnitPrice;
                item.UpdateDate = DateTime.Now;
                item.RowVersion = newItem.RowVersion;


                try
                {
                    await _uow.SaveChangesAsync(); 
                    return RedirectToAction("Index", new { so = so, query = query, pageIndex = pageIndex });
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    var exceptionEntry = ex.Entries.Single();
                    var clientValues = (Item)exceptionEntry.Entity;
                    var databaseEntry = exceptionEntry.GetDatabaseValues();

                    if (databaseEntry == null)
                    {
                        ModelState.AddModelError(string.Empty, ErrorMessageCommon.NullDatabaseItemEntityErrorMessage);
                    }
                    else
                    {
                        var databaseValues = (Item)databaseEntry.ToObject();
                        if (databaseValues.ItemName != clientValues.ItemName)
                            ModelState.AddModelError("ItemName", $"Current value: {databaseValues.ItemName}"); 
                        if (databaseValues.UnitPrice != clientValues.UnitPrice)
                            ModelState.AddModelError("UnitPrice", $"Current value: {databaseValues.UnitPrice}");
                        if (databaseValues.CategoryId != clientValues.CategoryId)
                        {
                            Category databaseCategory = await _uow.CategoryServices.FindByIdAsync(databaseValues.CategoryId);
                            ModelState.AddModelError("CategoryId", $"Current value: {databaseCategory.CategoryName}");
                        }


                        ModelState.AddModelError(string.Empty, "The record you attempted to edit "
                          + "was modified by another user after you got the original value. The "
                          + "edit operation was canceled and the current values in the database "
                          + "have been displayed. If you still want to edit this record, click "
                          + "the Save button again. Otherwise click the Back to List hyperlink.");

                        newItem.RowVersion = (byte[])databaseValues.RowVersion;
                        ModelState.Remove("RowVersion");
                        ViewBag.CatsId = _uow.CategoryServices.CategoryDropDownList(newItem.CategoryId);

                        ViewData["CurrentSort"] = so;
                        ViewData["CurrentFilter"] = query;
                        ViewData["CurrentPageIndex"] = pageIndex;
                        ViewData["CurrentTenantId"] = TenantId;

                        return View(newItem);
                    }
                }
            }
            else
                ModelState.AddModelError(string.Empty, ErrorMessageCommon.ModelStateErrorMessage);
            ViewBag.Cats = await _uow.CategoryServices.GetAllAsync();
            ViewBag.CatsId = _uow.CategoryServices.CategoryDropDownList(newItem.CategoryId);
            return View(newItem);
        }

        public async Task<IActionResult> Delete(int id, string so, string query, int pageIndex, bool? concurrencyError, int TenantId)
        {
            if (id == null)
                return NotFound();

            var item = await _uow.ItemServices.GetAllAsync(filter: i => i.ItemId == id, includeProperties: "Category");
            var targetItem = item.SingleOrDefault();

            if (targetItem == null)
            {
                if (concurrencyError.GetValueOrDefault())
                {
                    return RedirectToAction(nameof(Index), new { so = so, query = query, pageIndex = pageIndex });
                }
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

            return View(targetItem);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Item model, string so, string? query, int pageIndex, int TenantId)
        {
            try
            {
                if (await _uow.ItemServices.Any(i => i.ItemId == model.ItemId))
                {
                    _uow.ItemServices.Remove(model);
                    await _uow.SaveChangesAsync(); 
                }
                return RedirectToAction("Index", new { so = so, query = query, pageIndex = pageIndex });
            }
            catch (DbUpdateConcurrencyException ex)
            {
                _logger.LogError(ex.Message, $"Failed to delete item with Id: {model.ItemId}");
                return RedirectToAction(nameof(Delete), new { concurrencyError = true, id = model.ItemId, so = so, query = query, pageIndex = pageIndex ,TenantId=TenantId});
            }
        }
     
        public async Task<IActionResult> ShowItemOrders(int id, string so, string query, int pageIndex)
        {
            var item = await _uow.ItemServices.GetAllAsync(filter:i=>i.ItemId==id, includeProperties: "OrderDetail.Order");
            var targetItem = item.SingleOrDefault();

            if (id != null)
            {  
                var ItemOrdersDetail = new ShowAllItemOrdersViewModel()
                {
                    OrderDetails = targetItem.OrderDetail
                };

                ViewData["CurrentSort"] = so;
                ViewData["CurrentFilter"] = query;
                ViewData["CurrentPageIndex"] = pageIndex;

                return View(ItemOrdersDetail);
            }
            else
                return BadRequest();
        }
    
        public IActionResult UpdateItemPrice()
        {
            return View();
        }
         
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateItemPrice(int multiplier)
        {
            if (ModelState.IsValid)
            {
                if (multiplier != null)
                {
                    string Query = "UPDATE Items SET UnitPrice = UnitPrice * {0}";
                    ViewData["RowsAffected"] =
                        await _uow.DatabaseContext.ExecuteSqlRawAsync(Query, parameters: multiplier);

                    return View();
                }
                else
                    return BadRequest();
            }
            else
            {
                ModelState.AddModelError(string.Empty, ErrorMessageCommon.ModelStateErrorMessage);
                return View();
            }
            return View();
        }
    }
}
