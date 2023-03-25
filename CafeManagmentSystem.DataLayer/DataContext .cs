using CafeSystemManagement.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace CafeManagementSystem.DataLayer
{  

    public class DataContext : DbContext 
    {
        public int TenantId { get; set; }
        
        public DataContext(DbContextOptions<DataContext> options)
             : base(options)
        {   
        } 

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        { 
            modelBuilder.Entity<Item>()
            .HasQueryFilter(f => f.TenantId == TenantId);

            modelBuilder.Entity<User>()
           .HasQueryFilter(f => f.TenantId == TenantId);
        }
          
        public DbSet<Item> Items { get; set; } 
        public DbSet<User> Users { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Category> Categories { get; set; }
         
    }
}


