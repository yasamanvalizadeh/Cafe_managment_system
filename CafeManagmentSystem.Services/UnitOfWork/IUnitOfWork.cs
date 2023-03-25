using CafeManagmentSystem.Services.Contract;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace CafeManagmentSystem.Services.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IitemServices ItemServices { get; }
        IUserServices UserServices { get; }
        IOrderServices OrderServices { get; }
        IOrderDetailServices OrderDetailServices { get; }
        ICategoryServices CategoryServices { get; }
         
        DatabaseFacade DatabaseContext { get; } 
        int tenantId { get;} 
        int SaveChanges(); 
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken)); 
    }
}
