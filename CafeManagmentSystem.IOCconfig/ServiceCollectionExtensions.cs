using CafeManagementSystem.Common;
using CafeManagementSystem.DataLayer; 
using CafeManagmentSystem.Common.Multi_Tenancy;
using CafeManagmentSystem.DataLayer.ScpedFactory; 
using CafeManagmentSystem.Services.Contract; 
using CafeManagmentSystem.Services.Services;
using CafeManagmentSystem.Services.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;

namespace CafeManagmentSystem.IOCconfig
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection addCustomServices(this IServiceCollection services)
        { 
            services.AddTransient<IitemServices, ItemServices>();
            services.AddTransient<IUserServices, UserServices>();
            services.AddTransient<IOrderServices, OrderServices>();
            services.AddTransient<IOrderDetailServices, OrderDetailServices>();
            services.AddTransient<ICategoryServices, CategoryServices>(); 
            services.AddTransient<IUnitOfWork, UnitOfWork>();

            //Tenant resolution region
            //Get tenantId 
            services.AddScoped<ITenant>(sp =>
            {
                var tenantIdString = sp
                .GetRequiredService<IHttpContextAccessor>()
                .HttpContext
                .Request.Query["TenantId"];

                return tenantIdString != StringValues.Empty && int.TryParse(tenantIdString, out var tenantId)
                    ? new Tenant(tenantId)
                    : null;
            });

            //Register Singleton Context Factory
            var provider = services.BuildServiceProvider();
            var ConnectionStrings = provider.GetRequiredService<IOptionsMonitor<ConnectionString>>().CurrentValue;
            services.AddPooledDbContextFactory<DataContext>(
                    options =>
                    {
                        options.UseSqlServer(ConnectionStrings.CafeDBContextConnection);
                    });

            //Register scoped context factory
            services.AddScoped<DataContextScopedFactory>();


            // Register DBcontext
            services.AddScoped(
                sp => sp
                .GetRequiredService<DataContextScopedFactory>()
                .CreateDbContext());

            //End region

             
            return services;
        }
        
    }
}