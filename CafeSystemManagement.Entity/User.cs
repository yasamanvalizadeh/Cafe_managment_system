using CafeManagementSystem.Common.CommonEntityProps;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeSystemManagement.Entities
{
    public class User : CommonEntityProps
    {
        [Key]
        public int UserId { get; set; }
        public int TenantId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; } 
        public string UserNumber { get; set; } 
        public string UserPassword { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }


        //relationship
        public virtual ICollection<Order> UserOrders { get; set; }   
    }
}
