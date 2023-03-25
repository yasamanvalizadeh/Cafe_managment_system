using CafeManagmentSystem.ViewModel;
using CafeSystemManagement.Entities;
using Microsoft.Azure.Management.ResourceManager.Fluent.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CafeManagmentSystem.Services.Contract
{
    public interface IGenericServices<Tentity> where Tentity : class
    {
        Task AddAsync(Tentity entity); 
        Task<IEnumerable<Tentity>> GetAllAsync(
             Expression<Func<Tentity, bool>> filter = null,
             Expression<Func<Tentity, object>> orderBy = null,
             Expression<Func<Tentity, object>> OrderByDescending =null, 
             string includeProperties = "", 
             int tenantId = -1);
        
        void Update(Tentity entity);
        void Remove(Tentity entity);
        Task<Tentity> FindByIdAsync(int id);
        void RemoveById(int id);
        Task<bool> Any(Expression<Func<Tentity, bool>> predicate);
         
    }
}
