using CafeManagementSystem.DataLayer;
using CafeManagmentSystem.Repositories.Contract;
using CafeManagmentSystem.Repositories.Repository;
using CafeSystemManagement.Entities;

namespace CafeManagmentSystem.Services.Services
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(DataContext db)
            :base(db) { } 
    }
}
