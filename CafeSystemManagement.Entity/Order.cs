using CafeManagementSystem.Common.CommonEntityProps;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks; 

namespace CafeSystemManagement.Entities
{
    public class Order : CommonEntityProps
    {
        [Key]
        public int OrderId { get; set; }    
        public string OrderStatus { get; set; } = "In Progress";
         

        //foreign keys
        public int UserId { get; set; } 

        //relationShip
        public virtual User User { get; set; }

        public virtual ICollection<OrderDetail> OrderDetail { get; set; }
 
    }
}
