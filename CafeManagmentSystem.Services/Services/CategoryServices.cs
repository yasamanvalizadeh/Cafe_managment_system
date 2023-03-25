using CafeManagementSystem.DataLayer; 
using CafeManagmentSystem.Services.Contract; 
using CafeSystemManagement.Entities;
using Microsoft.AspNetCore.Mvc.Rendering; 

namespace CafeManagmentSystem.Services.Services
{
    public class CategoryServices : GenericServices<Category>, ICategoryServices
    {  
        public CategoryServices(DataContext db)
            : base(db) { }
        public SelectList CategoryDropDownList(object selectedCategory = null)
        {
            var catsQuery = from c in _db.Set<Category>()
                            orderby c.CategoryName
                            select c;

            return new SelectList(catsQuery, "CategoryId", "CategoryName", selectedCategory);
            
        }
    }
}
