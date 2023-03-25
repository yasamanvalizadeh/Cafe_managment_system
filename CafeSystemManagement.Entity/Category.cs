using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeSystemManagement.Entities
{
    public class Category
    {
        public int CategoryId { get; set; }  
        public string CategoryName { get; set; }  

        //relationship
        public virtual ICollection<Item> Items { get; set; }
         
    }
}
