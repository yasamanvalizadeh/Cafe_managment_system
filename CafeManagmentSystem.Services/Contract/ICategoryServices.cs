using CafeSystemManagement.Entities;
using Microsoft.AspNetCore.Mvc.Rendering; 

namespace CafeManagmentSystem.Services.Contract
{
    public interface ICategoryServices : IGenericServices<Category>
    {
        SelectList CategoryDropDownList(object selectedCategory = null);
    }
}
