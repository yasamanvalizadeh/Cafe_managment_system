
using CafeManagementSystem.DataLayer;
using CafeManagmentSystem.Common.Multi_Tenancy;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeManagmentSystem.DataLayer.ScpedFactory
{ 
    public class DataContextScopedFactory : IDbContextFactory<DataContext>
    {
        private const int DefaultTenantId = -1;
        private readonly IDbContextFactory<DataContext> _pooledFactory;
        private readonly int _tenantId;

        public DataContextScopedFactory(
                IDbContextFactory<DataContext> pooledFactory,
                ITenant tenant)
        {
            _pooledFactory = pooledFactory;
            _tenantId = tenant?.TenantId ?? DefaultTenantId;
        }
        public DataContext CreateDbContext()
        {
            var context = _pooledFactory.CreateDbContext();
            context.TenantId = _tenantId;
            return context;
        }
    }
}
