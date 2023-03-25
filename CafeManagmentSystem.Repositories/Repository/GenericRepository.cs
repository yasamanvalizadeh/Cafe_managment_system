using CafeManagementSystem.DataLayer;
using CafeManagmentSystem.Repositories.Contract;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CafeManagmentSystem.Repositories.Repository
{ 
    public class GenericRepository<Tentity> : IGenericRepository<Tentity> where Tentity : class
    {
        protected readonly DataContext _db;
        internal DbSet<Tentity> _dbSet;


        public GenericRepository(DataContext db)
        {
            db = db;
            _dbSet = _db.Set<Tentity>();   
        }

        public async Task<Tentity> GetAsync(Expression<Func<Tentity, bool>> expression, CancellationToken cancellationToken = default)
           => await _dbSet.FirstOrDefaultAsync(expression, cancellationToken);

        public async Task AddAsync(Tentity entity)
        => await _dbSet.AddAsync(entity);

        public async Task<Tentity> FindByIdAsync(int id)
        => await _dbSet.FindAsync(id);

        public async Task<IEnumerable<Tentity>> GetAllAsync(
            Expression<Func<Tentity, bool>> filter = null, 
            Func<IQueryable<Tentity>, IOrderedQueryable<Tentity>> orderBy = null, 
            string includeProperties = ""
            )
        {
            IQueryable<Tentity> query = _dbSet;

            if (filter != null)
                query = query.Where(filter);

            foreach (var navigationProperty in includeProperties.Split
               (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(navigationProperty);
            }

            if (orderBy != null)
            {
                return await orderBy(query).ToListAsync();
            }
            else
            {
                return await query.ToListAsync();
            }
        }

        public void Remove(Tentity entity)
        {
            if (_db.Entry(entity).State == EntityState.Detached)
            {
                _dbSet.Attach(entity);
            }
            _dbSet.Remove(entity);
        } 

        public async void RemoveById(int id)
        {
            Tentity targetEntity = await _dbSet.FindAsync(id);
            _dbSet.Remove(targetEntity);
        }


        public void Update(Tentity entity)
        {
            _dbSet.Attach(entity);
            _db.Entry(entity).State = EntityState.Modified;
        }

        public async Task<bool> Any(
            Expression<Func<Tentity, bool>> predicate )
        {
            return await _dbSet.AnyAsync(predicate);
        }
        
        public async Task<int> ExecuteSqlRawAsync(string sql, params object[] parameters)
        {
            return await _dbSet.Database.ExecuteSqlRawAsync(sql,parameters);
        }
    }
}
