using CafeManagementSystem.DataLayer; 
using CafeManagmentSystem.Services.Contract; 
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq.Expressions; 

namespace CafeManagmentSystem.Services.Services
{
    public class GenericServices<Tentity> : IGenericServices<Tentity> where Tentity : class 
    {
        protected readonly DataContext _db;
        private readonly DbSet<Tentity> _dbset;

        public GenericServices(DataContext db)
        {
            _db = db;
            _dbset = _db.Set<Tentity>();
        }

        public async Task AddAsync(Tentity entity)
        => await _dbset.AddAsync(entity);

        public async Task<Tentity> FindByIdAsync(int id)
        => await _dbset.FindAsync(id);

        public async Task<IEnumerable<Tentity>> GetAllAsync(
            Expression<Func<Tentity, bool>> filter = null,
            Expression<Func<Tentity, object>> orderBy = null,
            Expression<Func<Tentity, object>> OrderByDescending = null,
            string includeProperties = "",
            int tenantId = -1)
        {
            IQueryable<Tentity> query = _dbset;

            if (filter != null)
                query = query.Where(filter);

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                if (!string.IsNullOrWhiteSpace(includeProperty))
                    query = query.Include(includeProperty);
            }

            if (tenantId == -1)
                query = query.IgnoreQueryFilters();

            if (OrderByDescending != null)
            {
                query = query.OrderByDescending(OrderByDescending);
                return await query.ToListAsync();
            }

            else if(orderBy != null)
            {
                query = query.OrderBy(orderBy);
                return await query.ToListAsync();
            }
            else
            {
                return await query.ToListAsync();
            }
        }
        public void Remove(Tentity entity)
        => _dbset.Remove(entity);

        public void Update(Tentity entity)
        => _dbset.Update(entity);

        public async void RemoveById(int id)
        {
            Tentity tentity = await _dbset.FindAsync(id);
            _dbset.Remove(tentity);
        }

        public async Task<bool> Any(Expression<Func<Tentity, bool>> predicate)
        => await _dbset.AnyAsync(predicate);
        
    }
} 
