using CafeManagementSystem.DataLayer;
using CafeManagmentSystem.Services.Contract; 
using Microsoft.EntityFrameworkCore.Infrastructure;  

namespace CafeManagmentSystem.Services.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _db;
        public IitemServices ItemServices { get; }
        public IUserServices UserServices { get; }
        public IOrderServices OrderServices { get; }
        public IOrderDetailServices OrderDetailServices { get; }
        public ICategoryServices CategoryServices { get; }
        
        public UnitOfWork(DataContext db,
            IitemServices itemServices,
            IUserServices userServices,
            IOrderServices orderServices,
            IOrderDetailServices orderDetailServices,
            ICategoryServices categoryServices)
        {
            this._db = db;
            this.ItemServices = itemServices;
            this.UserServices = userServices;
            this.OrderServices = orderServices;
            this.OrderDetailServices = orderDetailServices;
            this.CategoryServices = categoryServices; 
        } 

        public DatabaseFacade DatabaseContext
        {
            get
            {
                return _db.Database;
            }
        } 

        public int tenantId
        {
            get
            {
                return _db.TenantId;
            }
        } 

        public int SaveChanges()
            => _db.SaveChanges();

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
            => _db.SaveChangesAsync();

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
                _db.Dispose();
        }
    }
}
