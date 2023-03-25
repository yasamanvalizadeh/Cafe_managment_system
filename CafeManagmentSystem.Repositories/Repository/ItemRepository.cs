using CafeManagementSystem.DataLayer;
using CafeManagmentSystem.Repositories.Contract;
using CafeSystemManagement.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeManagmentSystem.Repositories.Repository
{
    public class ItemRepository : GenericRepository<Item> ,  IitemRepository
    {
        public ItemRepository(DataContext db)
            :base(db)
        { 
        }
    }
}
