using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CafeManagmentSystem.Repositories.Contract
{
    public interface IGenericRepository<Tentity> where Tentity : class
    {
        Task<Tentity> GetAsync(Expression<Func<Tentity, bool>> expression,
                                CancellationToken cancellationToken = default);
        Task AddAsync(Tentity entity);
        Task<IEnumerable<Tentity>> GetAllAsync(
            Expression<Func<Tentity, bool>> filter = null,
            Func<IQueryable<Tentity>, IOrderedQueryable<Tentity>> orderBy = null,
            string includeProperties = "");
        void Update(Tentity entity);
        void Remove(Tentity entity);
        Task<Tentity> FindByIdAsync(int id);
        void RemoveById(int id);
        Task<bool> Any(Expression<Func<Tentity, bool>> predicate);
    }
}
